using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VitalCareWeb.Services.Article;
using VitalCareWeb.Services.ArticleCategory;
using VitalCareWeb.Services.Doctor;
using VitalCareWeb.Services.Location;
using VitalCareWeb.Services.Serivice;
using VitalCareWeb.Services.Speciality;
using VitalCareWeb.Services.Tag;
using VitalCareWeb.ViewModels;
using VitalCareWeb.ViewModels.Article;
using VitalCareWeb.ViewModels.ArticleCategory;
using VitalCareWeb.ViewModels.Doctor;
using VitalCareWeb.ViewModels.Location;
using VitalCareWeb.ViewModels.Service;
using VitalCareWeb.ViewModels.Speciality;
using VitalCareWeb.ViewModels.Tag;

namespace VitalCareWeb.Controllers;

public class HomeController : Controller
{
    private IMapper _mapper;
    private readonly ILogger<HomeController> _logger;
    private IServiceService _service;
    private IDoctorService _doctorService;
    private ISpecialityService _specialityService;
    private ILocationService _locationService;
    private IArticleService _articleService;
    private ITagService _tagService;
    private IArticleCategoryService _articleCategoryService;

    public HomeController(
        IMapper mapper,
        ILogger<HomeController> logger,
        IServiceService service,
        IDoctorService doctorService,
        ISpecialityService specialityService,
        ILocationService locationService,
        IArticleService articleService,
        ITagService tagService,
        IArticleCategoryService articleCategoryService

    )
    {
        _mapper = mapper;
        _logger = logger;
        _service = service;
        _doctorService = doctorService;
        _specialityService = specialityService;
        _locationService = locationService;
        _articleService = articleService;
        _tagService = tagService;
        _articleCategoryService = articleCategoryService;

    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var viewModel = new HomePageViewModel();

        var services = await _service.GetAll();
        viewModel.Services = _mapper.Map<List<ServiceViewModel>>(services);

        var doctors = await _doctorService.GetAll();
        viewModel.Doctors = _mapper.Map<List<DoctorViewModel>>(doctors);

        var articles = await _articleService.GetRandomArticles();
        viewModel.Articles = _mapper.Map<List<ArticleViewModel>>(articles);

        return View(viewModel);
    }

    [HttpGet]
    public IActionResult AboutUs()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Services()
    {
        var services = await _service.GetAll();
        var viewModels = _mapper.Map<List<ServiceViewModel>>(services);
        return View(viewModels);
    }

    [HttpGet]
    public async Task<IActionResult> Service(int id)
    {
        try
        {
            var service = await _service.GetById(id);

            if (service == null)
            {
                return View("_ServiceNotFound");
            }

            var viewModel = _mapper.Map<ServiceViewModel>(service);
            return View(viewModel);
        }
        catch (Exception ex)
        {
            return PartialView("_ServiceLoadingError");
        }
    }

    [HttpGet]
    public async Task<IActionResult> Doctors()
    {
        var viewModel = new DoctorPageViewModel();
        var specialities = _mapper.Map<List<SpecialityViewModel>>(await _specialityService.GetAll());
        var locations = _mapper.Map<List<LocationViewModel>>(await _locationService.GetAll());

        viewModel.Specialities = specialities;
        viewModel.Locations = locations;

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> FilterDoctors([FromBody] DoctorsFilterViewModel model)
    {
        var doctorList = _mapper.Map<List<DoctorViewModel>>(await _doctorService.GetAllFiltered(model.DoctorName, model.Locations, model.Genders, model.Specialities));

        return PartialView("_FilteredDoctors", doctorList);
    }


    [HttpGet]
    public async Task<IActionResult> Articles()
    {
        var articles = await _articleService.GetAll();
        var articleList = _mapper.Map<List<ArticleViewModel>>(articles);
        var latestArticleList = articleList.OrderByDescending(r => r.IsPublished).Take(4).ToList();

        var tags = _mapper.Map<List<TagViewModel>>(await _tagService.GetAll());
        var categories = _mapper.Map<List<ArticleCategoryViewModel>>(await _articleCategoryService.GetAll());

        var viewModel = new ArticlesPageViewModel();
        viewModel.AllArticles = articleList;
        viewModel.LatestArticles = latestArticleList;
        viewModel.Tags = tags;
        viewModel.Categories = categories;

        return View(viewModel);
    }

    [HttpGet]
    public async Task<IActionResult> Article(int id)
    {
        var articles = await _articleService.GetAll();
        var articleList = _mapper.Map<List<ArticleViewModel>>(articles);
        var latestArticleList = articleList.OrderByDescending(r => r.IsPublished).Take(4).ToList();

        var tags = _mapper.Map<List<TagViewModel>>(await _tagService.GetAll());
        var categories = _mapper.Map<List<ArticleCategoryViewModel>>(await _articleCategoryService.GetAll());

        var viewModel = new ArticlesPageViewModel();
        viewModel.AllArticles = articleList;
        viewModel.LatestArticles = latestArticleList;
        viewModel.Tags = tags;
        viewModel.Categories = categories;
        viewModel.Article = articleList.FirstOrDefault(r => r.Id == id);

        return View(viewModel);
    }

    [HttpGet]
    public IActionResult ContactUs()
    {
        return View();
    }

}