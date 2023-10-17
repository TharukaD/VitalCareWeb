using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VitalCareWeb.Entities;
using VitalCareWeb.Services.Speciality;
using VitalCareWeb.ViewModels;
using VitalCareWeb.ViewModels.Speciality;

namespace VitalCareWeb.Controllers
{
    public class SpecialitiesController : Controller
    {
        private IMapper _mapper;
        private ISpecialityService _specialityService;
        private readonly ILogger<SpecialitiesController> _logger;

        public SpecialitiesController(
            IMapper mapper,
            ILogger<SpecialitiesController> logger,
            ISpecialityService specialityService
            )
        {
            _mapper = mapper;
            _logger = logger;
            _specialityService = specialityService;
        }

        #region Index
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<SpecialityViewModel>? output = new();

            var specialities = await _specialityService.GetAll();
            output = _mapper.Map<List<SpecialityViewModel>>(specialities);

            return View(output);
        }
        #endregion


        #region Add [ HttpGet ]
        [HttpGet]
        public IActionResult Add()
        {
            return PartialView("_AddEdit");
        }
        #endregion

        #region Add [ HttpPost ]
        [HttpPost]
        public async Task<IActionResult> Add(AddEditSpecialityViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Validations failed."));
                }

                if (await _specialityService.IsDublicate(0, viewModel.Name.Trim()) == true)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Speciality name already exist"));
                }

                var speciality = _mapper.Map<AddEditSpecialityViewModel, Speciality>(viewModel);

                await _specialityService.Add(speciality);
                return PartialView("_AjaxActionResult", new AjaxActionResult(true, "Successfully added.", "", true));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Failed to add."));
            }
        }
        #endregion


        #region Edit [ HttpGet ]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var speciality = await _specialityService.GetById(id);

                if (speciality == null)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Speciality not found."));
                }

                var viewModel = _mapper.Map<AddEditSpecialityViewModel>(speciality);
                return PartialView("_AddEdit", viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return PartialView("_AjaxActionResult", new AjaxActionResult(false, ex.Message));
            }
        }
        #endregion

        #region Edit [ HttpPost ]
        [HttpPost]
        public async Task<IActionResult> Edit(AddEditSpecialityViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Validations failed."));
                }

                if (await _specialityService.IsDublicate(viewModel.Id.Value, viewModel.Name.Trim()) == true)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Speciality name already exist"));
                }

                var speciality = _mapper.Map<AddEditSpecialityViewModel, Speciality>(viewModel);

                await _specialityService.Update(speciality);
                return PartialView("_AjaxActionResult", new AjaxActionResult(true, "Successfully saved.", "", true));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Failed to save."));
            }
        }
        #endregion

    }
}
