using Microsoft.AspNetCore.Mvc;

namespace VitalCareWeb.Controllers
{
	public class AdminController : Controller
	{
		public IActionResult Index()
		{
			return RedirectToAction("Index", "Services");
		}
	}
}
