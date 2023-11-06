using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VitalCareWeb.Entities;
using VitalCareWeb.Services.Appointment;
using VitalCareWeb.Services.AppointmentReason;
using VitalCareWeb.Services.Article;
using VitalCareWeb.Services.ArticleCategory;
using VitalCareWeb.Services.Brand;
using VitalCareWeb.Services.CounterRecord;
using VitalCareWeb.Services.Doctor;
using VitalCareWeb.Services.EmailService;
using VitalCareWeb.Services.HomePageBanner;
using VitalCareWeb.Services.Inquiry;
using VitalCareWeb.Services.Location;
using VitalCareWeb.Services.Serivice;
using VitalCareWeb.Services.Speciality;
using VitalCareWeb.Services.Tag;
using VitalCareWeb.Services.WhyChooseUsRecord;
using VitalCareWeb.Utlity;
using VitalCareWeb.ViewModels;
using VitalCareWeb.ViewModels.Appoinment;
using VitalCareWeb.ViewModels.AppointmentReason;
using VitalCareWeb.ViewModels.Article;
using VitalCareWeb.ViewModels.ArticleCategory;
using VitalCareWeb.ViewModels.Brand;
using VitalCareWeb.ViewModels.CounterRecord;
using VitalCareWeb.ViewModels.Doctor;
using VitalCareWeb.ViewModels.HomePageBanner;
using VitalCareWeb.ViewModels.Inquiry;
using VitalCareWeb.ViewModels.Location;
using VitalCareWeb.ViewModels.Service;
using VitalCareWeb.ViewModels.Speciality;
using VitalCareWeb.ViewModels.Tag;
using VitalCareWeb.ViewModels.WhyChooseUsRecord;

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
    private IInquiryService _inquiryService;
    private IEmailService _emailService;
    private IWhyChooseUsRecordService _whyChooseUsRecordService;
    private ICounterRecordService _counterRecordService;
    private IBrandService _brandService;
    private IHomePageBannerService _homePageBannerService;
    private readonly IConfiguration _configuration;

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
        IAppointmentService appointmentService,
        IInquiryService inquiryService,
        IEmailService emailService,
        IWhyChooseUsRecordService whyChooseUsRecordService,
        ICounterRecordService counterRecordService,
        IBrandService brandService,
        IHomePageBannerService homePageBannerService,
        IConfiguration configuration
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
        _inquiryService = inquiryService;
        _emailService = emailService;
        _configuration = configuration;
        _whyChooseUsRecordService = whyChooseUsRecordService;
        _counterRecordService = counterRecordService;
        _brandService = brandService;
        _homePageBannerService = homePageBannerService;
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
        viewModel.Locations = _mapper.Map<List<LocationViewModel>>(locations);

        var whyChooseUsRecords = await _whyChooseUsRecordService.GetAll();
        viewModel.WhyChooseUsRecords = _mapper.Map<List<WhyChooseUsRecordViewModel>>(whyChooseUsRecords);

        var counterRecords = await _counterRecordService.GetAll();
        viewModel.CounterRecords = _mapper.Map<List<CounterRecordViewModel>>(counterRecords);

        var brands = await _brandService.GetAll();
        viewModel.Brands = _mapper.Map<List<BrandViewModel>>(brands);

        var banners = await _homePageBannerService.GetAll();
        viewModel.Banners = _mapper.Map<List<HomePageBannerViewModel>>(banners);

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

    [HttpGet]
    public async Task<IActionResult> AllBrands()
    {
        var allBrands = _mapper.Map<IList<BrandViewModel>>(await _brandService.GetAll());
        return View(allBrands);
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
    public async Task<IActionResult> ContactUs()
    {
        var locations = _mapper.Map<IEnumerable<LocationViewModel>>(await _locationService.GetAll());
        var viewModel = new ContactUsPageViewModel();
        viewModel.Initialize(locations);
        return View(viewModel);
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
    public async Task<JsonResult> LoadDoctorsBySpecialityId(int specialityId)
    {
        var specialityList = new List<int>();
        specialityList.Add(specialityId);
        var doctors = await _doctorService.GetAllFiltered(null, null, null, specialityList);
        var query = doctors.ToList()
            .Select(dep => new
            {
                key = dep.Id,
                name = dep.Name,
            });
        return Json(query);
    }

    [HttpGet]
    public async Task<IActionResult> AppoinmentSuccess(int id)
    {
        bool appointmentIsExist = false;
        string appointmentNumber = "";
        var appointment = await _appointmentService.GetById(id);
        if (appointment != null)
        {
            appointmentIsExist = true;
            appointmentNumber = HelperMethods.ReturnAppointmentNo(id);
        }
        ViewBag.AppointmentIsExist = appointmentIsExist;
        ViewBag.AppointmentNumber = appointmentNumber;
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

            var appointmentViewModel = _mapper.Map<AppointmentViewModel>(appointment);
            SendAppointmentMailWhenCreated(appointmentViewModel);

            return RedirectToAction("AppoinmentSuccess", new { id = appointment.Id });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return RedirectToAction("AppoinmentFailed");
        }
    }


    private void SendAppointmentMailWhenCreated(AppointmentViewModel viewModel)
    {
        var emailBody =
            $"<table style=\"border: 1px solid; border-collapse: collapse; width:100%\">" +
                $"<tr style=\"border: 1px solid\">" +
                     $"<td style=\"border: 1px solid; padding: 7px\">Number</td>" +
                     $"<td style=\"border: 1px solid; padding: 7px\">{viewModel.AppointmentNo}</td>" +
                $"</tr>" +
                $"<tr style=\"border: 1px solid\">" +
                     $"<td style=\"border: 1px solid; padding: 7px\">Name</td>" +
                     $"<td style=\"border: 1px solid; padding: 7px\">{viewModel.Name}</td>" +
                $"</tr>" +
                $"<tr style=\"border: 1px solid\">" +
                     $"<td style=\"border: 1px solid; padding: 7px\">Phone No</td>" +
                     $"<td style=\"border: 1px solid; padding: 7px\">{viewModel.PhoneNo}</td>" +
                $"</tr>" +
                $"<tr style=\"border: 1px solid\">" +
                     $"<td style=\"border: 1px solid; padding: 7px\">Id Card / Passport</td>" +
                     $"<td style=\"border: 1px solid; padding: 7px\">{viewModel.IdentityNo}</td>" +
                $"</tr>" +
                $"<tr style=\"border: 1px solid\">" +
                     $"<td style=\"border: 1px solid; padding: 7px\">Reason For Visit</td>" +
                     $"<td style=\"border: 1px solid; padding: 7px\">{viewModel.ReasonForVisit}</td>" +
                $"</tr>" +
                $"<tr style=\"border: 1px solid\">" +
                     $"<td style=\"border: 1px solid; padding: 7px\">Speciality</td>" +
                     $"<td style=\"border: 1px solid; padding: 7px\">{viewModel.SpecialityName}</td>" +
                $"</tr>" +
                $"<tr style=\"border: 1px solid\">" +
                     $"<td style=\"border: 1px solid; padding: 7px\">Doctor</td>" +
                     $"<td style=\"border: 1px solid; padding: 7px\">{viewModel.DoctorName}</td>" +
                $"</tr>" +
                $"<tr style=\"border: 1px solid\">" +
                     $"<td style=\"border: 1px solid; padding: 7px\">Prefered Date</td>" +
                     $"<td style=\"border: 1px solid; padding: 7px\">{viewModel.PreferredDateTimeString}</td>" +
                $"</tr>" +
                $"<tr style=\"border: 1px solid\">" +
                     $"<td style=\"border: 1px solid; padding: 7px\">Created Date</td>" +
                     $"<td style=\"border: 1px solid; padding: 7px\">{viewModel.CreatedDateString}</td>" +
                $"</tr>" +
            $"</table>";

        var emailHTML = _emailService.GetHTMLEmailContent(
            "Appointment Request",
        emailBody
        );

        string emailAddress = _configuration["AppointmentRedirectMailAddress"];

        var isEmailSent = _emailService.SendEmail(
            new EmailDto
            {
                To = emailAddress,
                Subject = "Appointment Reques",
                Body = emailHTML
            });
    }

    #endregion

    #region Inquiry
    [HttpGet]
    public IActionResult InquirySuccess(int id)
    {
        return View();
    }

    [HttpGet]
    public IActionResult InquiryFailed()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateInquery(ContactUsPageViewModel viewModel)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("ContactUs");
            }

            var inquiry = _mapper.Map<Inquiry>(viewModel.CreateInquiryViewModel);
            inquiry.CreatedOn = DateTime.Now;

            await _inquiryService.Add(inquiry);
            var inquiryViewModel = _mapper.Map<InquiryViewModel>(inquiry);

            SendInquiryMailWhenCreated(inquiryViewModel);

            return RedirectToAction("InquirySuccess");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return RedirectToAction("AppoinmentFailed");
        }
    }


    private void SendInquiryMailWhenCreated(InquiryViewModel viewModel)
    {
        var emailBody =
            $"<table style=\"border: 1px solid; border-collapse: collapse; width:100%\">" +
                $"<tr style=\"border: 1px solid\">" +
                     $"<td style=\"border: 1px solid; padding: 7px\">Name</td>" +
                     $"<td style=\"border: 1px solid; padding: 7px\">{viewModel.Name}</td>" +
                $"</tr>" +
                $"<tr style=\"border: 1px solid\">" +
                     $"<td style=\"border: 1px solid; padding: 7px\">Email Address</td>" +
                     $"<td style=\"border: 1px solid; padding: 7px\">{viewModel.EmailAddress}</td>" +
                $"</tr>" +
                $"<tr style=\"border: 1px solid\">" +
                     $"<td style=\"border: 1px solid; padding: 7px\">Phone Number</td>" +
                     $"<td style=\"border: 1px solid; padding: 7px\">{viewModel.PhoneNo}</td>" +
                $"</tr>" +
                $"<tr style=\"border: 1px solid\">" +
                     $"<td style=\"border: 1px solid; padding: 7px\">Message</td>" +
                     $"<td style=\"border: 1px solid; padding: 7px\">{viewModel.Message}</td>" +
                $"</tr>" +
                $"<tr style=\"border: 1px solid\">" +
                     $"<td style=\"border: 1px solid; padding: 7px\">Created Date</td>" +
                     $"<td style=\"border: 1px solid; padding: 7px\">{viewModel.CreatedOnString}</td>" +
                $"</tr>" +
            $"</table>";

        var emailHTML = _emailService.GetHTMLEmailContent(
            "Inquiry Request",
            emailBody
        );

        string emailAddress = _configuration["AppointmentRedirectMailAddress"];

        var isEmailSent = _emailService.SendEmail(
            new EmailDto
            {
                To = emailAddress,
                Subject = "Appointment Reques",
                Body = emailHTML
            });
    }


    #endregion
}