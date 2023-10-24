using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VitalCareWeb.Entities;
using VitalCareWeb.Services.Appointment;
using VitalCareWeb.Services.AppointmentReason;
using VitalCareWeb.Services.Article;
using VitalCareWeb.Services.ArticleCategory;
using VitalCareWeb.Services.Doctor;
using VitalCareWeb.Services.Location;
using VitalCareWeb.Services.Serivice;
using VitalCareWeb.Services.Speciality;
using VitalCareWeb.Services.Tag;
using VitalCareWeb.ViewModels;
using VitalCareWeb.ViewModels.Appoinment;
using VitalCareWeb.ViewModels.AppointmentReason;
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
    private IAppointmentReasonService _appointmentReasonService;
    private IAppointmentService _appointmentService;

    public HomeController(
        IMapper mapper,
        ILogger<HomeController> logger,
        IServiceService service,
        IDoctorService doctorService,
        ISpecialityService specialityService,
        ILocationService locationService,
        IArticleService articleService,
        ITagService tagService,
        IArticleCategoryService articleCategoryService,
        IAppointmentReasonService appointmentReasonService,
        IAppointmentService appointmentService
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
        _appointmentReasonService = appointmentReasonService;
        _appointmentService = appointmentService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var viewModel = new HomePageViewModel();

        var doctors = await _doctorService.GetAll();
        viewModel.Doctors = _mapper.Map<List<DoctorViewModel>>(doctors);

        var articles = await _articleService.GetRandomArticles();
        viewModel.Articles = _mapper.Map<List<ArticleViewModel>>(articles);

        var serviceGroups = await ReturnAllServicesGroupWithLocations(null);
        viewModel.ServiceGroups = serviceGroups;

        var locations = await _locationService.GetAll();
        viewModel.Locations = _mapper.Map<List<LocationViewModel>>(locations); ;

        return View(viewModel);
    }

    [HttpGet]
    public async Task<IActionResult> AboutUs()
    {
        var locations = _mapper.Map<List<LocationViewModel>>(await _locationService.GetAll());
        return View(locations);
    }

    [HttpGet]
    public async Task<IActionResult> Services(int? locationId)
    {
        var serviceGroups = await ReturnAllServicesGroupWithLocations(locationId);
        return View(serviceGroups);
    }

    private async Task<List<ServiceGroupViewModel>> ReturnAllServicesGroupWithLocations(int? locationId)
    {
        var serviceGroups = new List<ServiceGroupViewModel>();

        var allServices = await _service.GetAll();
        allServices = allServices.OrderBy(x => x?.Location.Priority).ToList();

        var servicesList = _mapper.Map<List<ServiceViewModel>>(allServices);

        var groups = servicesList.GroupBy(x => x.LocationId).Select(grp => grp.ToList()).ToList();
        foreach (var group in groups)
        {
            var serviceGroup = new ServiceGroupViewModel();
            serviceGroup.LocationId = group[0].LocationId;
            serviceGroup.LocationName = group[0].LocationName;
            serviceGroup.Services = new List<ServiceViewModel>();
            serviceGroup.Services = group;

            serviceGroup.TabId = $"pills-tabs{group[0].LocationId}-tab";
            serviceGroup.TargetId = $"#pills-tabs{group[0].LocationId}";
            serviceGroup.AreaControlId = $"pills-tabs{group[0].LocationId}";

            serviceGroup.TabHeaderActiveClass = "";
            serviceGroup.TabContentActiveClass = "";

            serviceGroups.Add(serviceGroup);
        }

        if (serviceGroups.Any())
        {
            if (locationId == null)
            {
                serviceGroups[0].TabHeaderActiveClass = "active";
                serviceGroups[0].TabContentActiveClass = "show active";
            }
            else
            {
                var relatedServiceGroup = serviceGroups.SingleOrDefault(r => r.LocationId == locationId);
                if (relatedServiceGroup == null)
                {
                    serviceGroups[0].TabHeaderActiveClass = "active";
                    serviceGroups[0].TabContentActiveClass = "show active";
                }
                else
                {
                    relatedServiceGroup.TabHeaderActiveClass = "active";
                    relatedServiceGroup.TabContentActiveClass = "show active";
                }
            }
        }

        return serviceGroups;
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

    #region Appointment

    [HttpGet]
    public async Task<IActionResult> Appointment()
    {
        var reasons = _mapper.Map<List<AppointmentReasonViewModel>>(await _appointmentReasonService.GetAll());
        var specialities = _mapper.Map<List<SpecialityViewModel>>(await _specialityService.GetAll());
        var doctors = _mapper.Map<List<DoctorViewModel>>(await _doctorService.GetAll());

        var viewModel = new CreateAppoinmentViewModel();
        viewModel.Initialize(reasons, specialities, doctors);

        return View(viewModel);
    }

    [HttpGet]
    public async Task<IActionResult> AppoinmentSuccess(int id)
    {
        var appointment = await _appointmentService.GetById(id);
        if (appointment != null)
        {
            var viewModel = _mapper.Map<AppointmentViewModel>(appointment);
            return View(viewModel);
        }
        return View();
    }

    [HttpGet]
    public IActionResult AppoinmentFailed()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateAppoinment(CreateAppoinmentViewModel viewModel)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Appointment");
            }

            var reason = await _appointmentReasonService.GetById(viewModel.ReasonId);
            var speciality = await _specialityService.GetById(viewModel.SpecialityId);
            var doctor = await _doctorService.GetById(viewModel.DoctorId);

            var appointment = _mapper.Map<Appointment>(viewModel);
            appointment.ReasonForVisit = reason.Name;
            appointment.SpecialityName = speciality.Name;
            appointment.DoctorName = doctor.Name;
            appointment.CreatedDate = DateTime.Now;
            appointment.PreferredDateTime = new DateTime(
                year: viewModel.PreferredDate.Year,
                month: viewModel.PreferredDate.Month,
                day: viewModel.PreferredDate.Day,
                hour: viewModel.PreferredTime.Hour,
                minute: viewModel.PreferredTime.Minute,
                second: viewModel.PreferredTime.Second);

            await _appointmentService.Add(appointment);

            return RedirectToAction("AppoinmentSuccess", new { id = appointment.Id });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return RedirectToAction("AppoinmentFailed");
        }
    }
    #endregion

}