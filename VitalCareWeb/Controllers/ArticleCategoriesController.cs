using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VitalCareWeb.Entities;
using VitalCareWeb.Services.ArticleCategory;
using VitalCareWeb.ViewModels;
using VitalCareWeb.ViewModels.ArticleCategory;

namespace VitalCareWeb.Controllers
{
    public class ArticleCategoriesController : Controller
    {
        private IMapper _mapper;
        private IArticleCategoryService _articleCategoryService;
        private readonly ILogger<ArticleCategoriesController> _logger;

        public ArticleCategoriesController(
            IMapper mapper,
            ILogger<ArticleCategoriesController> logger,
            IArticleCategoryService articleCategoryService
            )
        {
            _mapper = mapper;
            _logger = logger;
            _articleCategoryService = articleCategoryService;
        }

        #region Index
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<ArticleCategoryViewModel>? output = new();

            var categories = await _articleCategoryService.GetAll();
            output = _mapper.Map<List<ArticleCategoryViewModel>>(categories);

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
        public async Task<IActionResult> Add(AddEditArticleCategoryViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Validations failed."));
                }

                if (await _articleCategoryService.IsDublicate(0, viewModel.Name.Trim()) == true)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Category name already exist"));
                }

                var category = _mapper.Map<AddEditArticleCategoryViewModel, ArticleCategory>(viewModel);

                await _articleCategoryService.Add(category);
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
                var category = await _articleCategoryService.GetById(id);

                if (category == null)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Category not found."));
                }

                var viewModel = _mapper.Map<AddEditArticleCategoryViewModel>(category);
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
        public async Task<IActionResult> Edit(AddEditArticleCategoryViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Validations failed."));
                }

                if (await _articleCategoryService.IsDublicate(viewModel.Id.Value, viewModel.Name.Trim()) == true)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Category name already exist"));
                }

                var category = _mapper.Map<AddEditArticleCategoryViewModel, ArticleCategory>(viewModel);

                await _articleCategoryService.Update(category);
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
            var category = await _articleCategoryService.GetById(id);
            if (category == null)
            {
                return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Category not found."));
            }
            var viewModel = _mapper.Map<ArticleCategoryViewModel>(category);
            return PartialView("_Delete", viewModel);
        }
        #endregion

        #region Delete [ HttpPost ]
        [HttpPost]
        public async Task<IActionResult> Delete(ArticleCategoryViewModel viewModel)
        {
            try
            {
                var result = await _articleCategoryService.Delete(viewModel.Id);
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
