using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VitalCareWeb.Services.Appointment;
using VitalCareWeb.ViewModels.Appoinment;

namespace VitalCareWeb.Controllers
{
    public class AppointmentsController : Controller
    {
        private IMapper _mapper;
        private IAppointmentService _appointmentService;
        private readonly ILogger<AppointmentReasonsController> _logger;

        public AppointmentsController(
            IMapper mapper,
            ILogger<AppointmentReasonsController> logger,
            IAppointmentService appointmentService)
        {
            _mapper = mapper;
            _logger = logger;
            _appointmentService = appointmentService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<AppointmentViewModel>? output = new();

            var appointments = await _appointmentService.GetAll();
            output = _mapper.Map<List<AppointmentViewModel>>(appointments);

            return View(output);
        }
    }
}
