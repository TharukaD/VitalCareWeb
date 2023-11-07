using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VitalCareWeb.Entities;
using VitalCareWeb.Services.Tag;
using VitalCareWeb.ViewModels;
using VitalCareWeb.ViewModels.Tag;

namespace VitalCareWeb.Controllers
{
    [Authorize]
    public class TagsController : Controller
    {
        private IMapper _mapper;
        private ITagService _tagService;
        private readonly ILogger<TagsController> _logger;

        public TagsController(
            IMapper mapper,
            ILogger<TagsController> logger,
            ITagService tagService
            )
        {
            _mapper = mapper;
            _logger = logger;
            _tagService = tagService;
        }

        #region Index
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<TagViewModel>? output = new();

            var tags = await _tagService.GetAll();
            output = _mapper.Map<List<TagViewModel>>(tags);

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
        public async Task<IActionResult> Add(AddEditTagViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Validations failed."));
                }

                if (await _tagService.IsDublicate(0, viewModel.Name.Trim()) == true)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Tag name already exist"));
                }

                var tag = _mapper.Map<AddEditTagViewModel, Tag>(viewModel);

                await _tagService.Add(tag);
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
                var tag = await _tagService.GetById(id);

                if (tag == null)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Tag not found."));
                }

                var viewModel = _mapper.Map<AddEditTagViewModel>(tag);
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
        public async Task<IActionResult> Edit(AddEditTagViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Validations failed."));
                }

                if (await _tagService.IsDublicate(viewModel.Id.Value, viewModel.Name.Trim()) == true)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Tag name already exist"));
                }

                var tag = _mapper.Map<AddEditTagViewModel, Tag>(viewModel);

                await _tagService.Update(tag);
                return PartialView("_AjaxActionResult", new AjaxActionResult(true, "Successfully saved.", "", true));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Failed to save."));
            }
        }
        #endregion


        #region Delete [ HttpGet ]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var tag = await _tagService.GetById(id);
            if (tag == null)
            {
                return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Tag not found."));
            }
            var viewModel = _mapper.Map<TagViewModel>(tag);
            return PartialView("_Delete", viewModel);
        }
        #endregion

        #region Delete [ HttpPost ]
        [HttpPost]
        public async Task<IActionResult> Delete(TagViewModel viewModel)
        {
            try
            {
                var result = await _tagService.Delete(viewModel.Id);
                if (result.Item1 == true)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(true, "Successfully deleted.", "", true));
                }
                return PartialView("_AjaxActionResult", new AjaxActionResult(false, $"Failed to delete.[ {result.Item2} ]"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Failed to delete."));
            }
        }
        #endregion
    }
}
