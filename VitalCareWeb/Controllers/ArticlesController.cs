using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VitalCareWeb.Entities;
using VitalCareWeb.Services.Article;
using VitalCareWeb.Services.ArticleCategory;
using VitalCareWeb.Services.Tag;
using VitalCareWeb.ViewModels;
using VitalCareWeb.ViewModels.Article;
using VitalCareWeb.ViewModels.ArticleCategory;
using VitalCareWeb.ViewModels.Tag;

namespace VitalCareWeb.Controllers
{
    public class ArticlesController : Controller
    {
        private IMapper _mapper;
        private IArticleService _articleService;
        private IArticleCategoryService _articleCategoryService;
        private ITagService _tagService;
        private readonly ILogger<ArticlesController> _logger;

        public ArticlesController(
            IMapper mapper,
            ILogger<ArticlesController> logger,
            IArticleService articleService,
            IArticleCategoryService articleCategoryService,
            ITagService tagService
            )
        {
            _mapper = mapper;
            _logger = logger;
            _articleService = articleService;
            _articleCategoryService = articleCategoryService;
            _tagService = tagService;
        }

        #region Index
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<ArticleViewModel>? output = new();

            var services = await _articleService.GetAll();
            output = _mapper.Map<List<ArticleViewModel>>(services);

            return View(output);
        }
        #endregion


        #region Details
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var article = await _articleService.GetById(id);
            if (article == null)
                return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Article not found."));

            var viewModel = _mapper.Map<ArticleViewModel>(article);

            return View(viewModel);
        }
        #endregion


        #region Add [ HttpGet ]
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var viewModel = new AddEditArticleViewModel();

            var categoryList = _mapper.Map<IList<ArticleCategoryViewModel>>(await _articleCategoryService.GetAll());
            var tagList = _mapper.Map<IList<TagViewModel>>(await _tagService.GetAll());

            viewModel.Initialize(categoryList.ToList(), tagList.ToList());
            return PartialView("_AddEdit", viewModel);
        }
        #endregion

        #region Add [ HttpPost ]
        [HttpPost]
        public async Task<IActionResult> Add(AddEditArticleViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Validations failed."));
                }

                if (await _articleService.IsDublicate(0, viewModel.Title.Trim()) == true)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Article name already exist"));
                }

                var article = _mapper.Map<AddEditArticleViewModel, Article>(viewModel);

                await _articleService.Add(article);
                await _articleService.AssignTags(article.Id, viewModel.TagIds);
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
                var article = await _articleService.GetById(id);

                if (article == null)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Article not found."));
                }

                var categoryList = _mapper.Map<IList<ArticleCategoryViewModel>>(await _articleCategoryService.GetAll());
                var tagList = _mapper.Map<IList<TagViewModel>>(await _tagService.GetAll());

                var viewModel = _mapper.Map<AddEditArticleViewModel>(article);
                viewModel.InitializeForEdit(categoryList.ToList(), tagList.ToList(), article.ArticleTags.Select(r => r.TagId).ToList());

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
        public async Task<IActionResult> Edit(AddEditArticleViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Validations failed."));
                }

                if (await _articleService.IsDublicate(viewModel.Id.Value, viewModel.Title.Trim()) == true)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Article name already exist"));
                }

                var article = _mapper.Map<AddEditArticleViewModel, Article>(viewModel);

                await _articleService.Update(article);
                await _articleService.AssignTags(article.Id, viewModel.TagIds);
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
            var service = await _articleService.GetById(id);
            if (service == null)
            {
                return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Article not found."));
            }
            var viewModel = _mapper.Map<ArticleViewModel>(service);
            return PartialView("_Delete", viewModel);
        }
        #endregion

        #region Delete [ HttpPost ]
        [HttpPost]
        public async Task<IActionResult> Delete(ArticleViewModel viewModel)
        {
            try
            {
                var result = await _articleService.Delete(viewModel.Id);
                if (result.Item1 == true)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(true, "Successfully deleted.", "", true));
                }
                return PartialView("_AjaxActionResult", new AjaxActionResult(false, $"Failed to delete. {result.Item2}"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Failed to delete."));
            }
        }
        #endregion


        #region Upload Article Image [ HttpGet ]
        [HttpGet]
        public IActionResult UploadArticleImage(int id)
        {
            var viewModel = new AddArticleImageViewModel();
            viewModel.ArticleId = id;
            return PartialView("_UploadArticleImage", viewModel);
        }
        #endregion

        #region Upload Article Image [ HttpPost ]
        [HttpPost]
        public async Task<IActionResult> UploadArticleImage(AddArticleImageViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Validations failed."));
                }

                var article = await _articleService.GetById(viewModel.ArticleId);
                if (article == null)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Article not found."));
                }

                var file = viewModel.UploadedFile;

                var extension = file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                string filename = DateTime.Now.Ticks.ToString() + "." + extension;

                var filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\ArticleImages");

                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }

                var exactpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\ArticleImages", filename);
                using (var stream = new FileStream(exactpath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                article.Image = filename;
                await _articleService.Update(article);
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
