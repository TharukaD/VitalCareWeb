using Microsoft.AspNetCore.Mvc;

namespace VitalCareWeb.Controllers
{
    public class LocationsController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

    }
}
