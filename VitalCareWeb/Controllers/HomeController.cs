using AutoMapper;
using Microsoft.AspNetCore.Mvc;

using VitalCareWeb.Services.Doctor;
using VitalCareWeb.Services.Serivice;
using VitalCareWeb.Services.Speciality;

using VitalCareWeb.ViewModels;
using VitalCareWeb.ViewModels.Doctor;
using VitalCareWeb.ViewModels.Service;
using VitalCareWeb.ViewModels.Speciality;

namespace VitalCareWeb.Controllers;

public class HomeController : Controller
{
    private IMapper _mapper;
    private readonly ILogger<HomeController> _logger;
    private IServiceService _service;
    private IDoctorService _doctorService;
    private ISpecialityService _specialityService;

    public HomeController(
        IMapper mapper,
        ILogger<HomeController> logger,
        IServiceService service,
        IDoctorService doctorService,
        ISpecialityService specialityService
    )
    {
        _mapper = mapper;
        _logger = logger;
        _service = service;
        _doctorService = doctorService;
        _specialityService = specialityService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var viewModel = new HomePageViewModel();

        var services = await _service.GetAll();
        viewModel.Services = _mapper.Map<List<ServiceViewModel>>(services);

        var doctors = await _doctorService.GetAll();
        viewModel.Doctors = _mapper.Map<List<DoctorViewModel>>(doctors);

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

        var doctorList = _mapper.Map<List<DoctorViewModel>>(await _doctorService.GetAll());
        var specialities = _mapper.Map<List<SpecialityViewModel>>(await _specialityService.GetAll());

        viewModel.Doctors = doctorList;
        viewModel.Specialities = specialities;

        return View(viewModel);
    }

    [HttpGet]
    public IActionResult Articles()
    {

        return View();
    }

    [HttpGet]
    public IActionResult Article(int id)
    {
        return View();
    }

    [HttpGet]
    public IActionResult ContactUs()
    {
        return View();
    }

}