using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VitalCareWeb.Models;

namespace VitalCareWeb.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
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
			return View();
		}

		[HttpGet]
		public IActionResult Contact()
		{
			return View();
		}


		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}