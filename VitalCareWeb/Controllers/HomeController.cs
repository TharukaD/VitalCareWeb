using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VitalCareWeb.Data;
using VitalCareWeb.Services.Serivice;
using VitalCareWeb.ViewModels;
using VitalCareWeb.ViewModels.Service;

namespace VitalCareWeb.Controllers
{
    public class HomeController : Controller
    {
        private IMapper _mapper;
        private readonly ILogger<HomeController> _logger;
        private ApplicationDbContext _context;
        private IServiceService _service;

        public HomeController(IMapper mapper, ILogger<HomeController> logger, ApplicationDbContext context, IServiceService service)
        {
            _mapper = mapper;
            _logger = logger;
            _context = context;
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var viewModel = new HomePageViewModel();

            var services = await _service.GetAll();
            viewModel.Services = _mapper.Map<List<ServiceViewModel>>(services);

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
        public IActionResult Doctors()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Articles()
        {
            var articles = _context.Articles.Where(r => r.IsPublished);
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
}