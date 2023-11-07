using Microsoft.AspNetCore.Mvc;

namespace VitalCareWeb.Controllers
{
    public class ErrorController : Controller
    {
        [HttpGet]
        public IActionResult InternalServerError()
        {
            return View();
        }

        [HttpGet]
        public IActionResult NotFoundError()
        {
            return View();
        }
    }
}
