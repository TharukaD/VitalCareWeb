using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VitalCareWeb.Entities;
using VitalCareWeb.Services.Serivice;
using VitalCareWeb.ViewModels;
using VitalCareWeb.ViewModels.Service;

namespace VitalCareWeb.Controllers
{
    public class ServicesController : Controller
    {
        private IMapper _mapper;
        private IServiceService _service;
        private readonly ILogger<ServicesController> _logger;

        public ServicesController(
            IMapper mapper,
            ILogger<ServicesController> logger,
            IServiceService service
            )
        {
            _mapper = mapper;
            _logger = logger;
            _service = service;
        }

        #region Index
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<ServiceViewModel>? output = new();

            var services = await _service.GetAll();
            output = _mapper.Map<List<ServiceViewModel>>(services);

            return View(output);
        }
        #endregion


        #region Details
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var service = await _service.GetById(id);
            if (service == null)
                return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Service not found."));

            var viewModel = _mapper.Map<ServiceViewModel>(service);

            return View(viewModel);
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
        public async Task<IActionResult> Add(AddEditServiceViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Validations failed."));
                }

                if (await _service.IsDublicate(0, viewModel.Name.Trim()) == true)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Service name already exist"));
                }

                var service = _mapper.Map<AddEditServiceViewModel, Service>(viewModel);

                await _service.Add(service);
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
                var service = await _service.GetById(id);

                if (service == null)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Service not found."));
                }

                var viewModel = _mapper.Map<AddEditServiceViewModel>(service);
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
        public async Task<IActionResult> Edit(AddEditServiceViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Validations failed."));
                }

                if (await _service.IsDublicate(viewModel.Id.Value, viewModel.Name.Trim()) == true)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Service name already exist"));
                }

                var service = _mapper.Map<AddEditServiceViewModel, Service>(viewModel);

                await _service.Update(service);
                return PartialView("_AjaxActionResult", new AjaxActionResult(true, "Successfully saved.", "", true));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Failed to save."));
            }
        }
        #endregion


        #region Upload Service Image [ HttpGet ]
        [HttpGet]
        public IActionResult UploadServiceImage(int id)
        {
            var viewModel = new AddServiceImageViewModel();
            viewModel.ServiceId = id;
            return PartialView("_UploadServiceImage", viewModel);
        }
        #endregion

        #region Upload Service Image [ HttpPost ]
        [HttpPost]
        public async Task<IActionResult> UploadServiceImage(AddServiceImageViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Validations failed."));
                }

                var service = await _service.GetById(viewModel.ServiceId);
                if (service == null)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Person not found."));
                }

                var file = viewModel.UploadedFile;

                var extension = file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                string filename = DateTime.Now.Ticks.ToString() + "." + extension;

                var filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\ServiceImages");

                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }

                var exactpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\ServiceImages", filename);
                using (var stream = new FileStream(exactpath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                service.Image = filename;
                await _service.Update(service);
                return PartialView("_AjaxActionResult", new AjaxActionResult(true, "Successfully uploaded.", "", true));
            }
            catch (Exception ex)
            {
                return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Failed to upload."));
            }
        }
        #endregion

    }
}
