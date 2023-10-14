using Microsoft.AspNetCore.Mvc;
using VitalCareWeb.Data;

namespace VitalCareWeb.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private ApplicationDbContext _context;

		public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
		{
			_logger = logger;
			_context = context;
		}

		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public IActionResult AboutUs()
		{
			return View();
		}

		[HttpGet]
		public IActionResult Services()
		{
			return View();
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