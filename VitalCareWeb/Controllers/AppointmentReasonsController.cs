using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VitalCareWeb.Entities;
using VitalCareWeb.Services.AppointmentReason;
using VitalCareWeb.ViewModels;
using VitalCareWeb.ViewModels.AppointmentReason;

namespace VitalCareWeb.Controllers
{
    [Authorize]
    public class AppointmentReasonsController : Controller
    {
        private IMapper _mapper;
        private IAppointmentReasonService _appointmentReasonService;
        private readonly ILogger<AppointmentReasonsController> _logger;

        public AppointmentReasonsController(
            IMapper mapper,
            ILogger<AppointmentReasonsController> logger,
            IAppointmentReasonService appointmentReasonService
            )
        {
            _mapper = mapper;
            _logger = logger;
            _appointmentReasonService = appointmentReasonService;
        }

        #region Index
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<AppointmentReasonViewModel>? output = new();

            var services = await _appointmentReasonService.GetAll();
            output = _mapper.Map<List<AppointmentReasonViewModel>>(services);

            return View(output);
        }
        #endregion


        #region Add [ HttpGet ]
        [HttpGet]
        public IActionResult Add()
        {
            return PartialView("_AddEdit");
        }
        #endregion

        #region Add [ HttpPost ]
        [HttpPost]
        public async Task<IActionResult> Add(AddEditAppointmentReasonViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Validations failed."));
                }

                if (await _appointmentReasonService.IsDublicate(0, viewModel.Name.Trim()) == true)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Reason already exist"));
                }

                var reason = _mapper.Map<AddEditAppointmentReasonViewModel, AppointmentReason>(viewModel);

                await _appointmentReasonService.Add(reason);
                return PartialView("_AjaxActionResult", new AjaxActionResult(true, "Successfully added.", "", true));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Failed to add."));
            }
        }
        #endregion


        #region Edit [ HttpGet ]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var location = await _appointmentReasonService.GetById(id);

                if (location == null)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Reason not found."));
                }

                var viewModel = _mapper.Map<AddEditAppointmentReasonViewModel>(location);
                return PartialView("_AddEdit", viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return PartialView("_AjaxActionResult", new AjaxActionResult(false, ex.Message));
            }
        }
        #endregion

        #region Edit [ HttpPost ]
        [HttpPost]
        public async Task<IActionResult> Edit(AddEditAppointmentReasonViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Validations failed."));
                }

                if (await _appointmentReasonService.IsDublicate(viewModel.Id.Value, viewModel.Name.Trim()) == true)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Reason already exist"));
                }

                var reason = _mapper.Map<AddEditAppointmentReasonViewModel, AppointmentReason>(viewModel);

                await _appointmentReasonService.Update(reason);
                return PartialView("_AjaxActionResult", new AjaxActionResult(true, "Successfully saved.", "", true));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Failed to save."));
            }
        }
        #endregion


        #region Delete [ HttpGet ]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var reason = await _appointmentReasonService.GetById(id);
            if (reason == null)
            {
                return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Reason not found."));
            }
            var viewModel = _mapper.Map<AppointmentReasonViewModel>(reason);
            return PartialView("_Delete", viewModel);
        }
        #endregion

        #region Delete [ HttpPost ]
        [HttpPost]
        public async Task<IActionResult> Delete(AppointmentReasonViewModel viewModel)
        {
            try
            {
                var result = await _appointmentReasonService.Delete(viewModel.Id);
                if (result == true)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(true, "Successfully deleted.", "", true));
                }
                return PartialView("_AjaxActionResult", new AjaxActionResult(false, $"Failed to delete."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Failed to delete."));
            }
        }
        #endregion
    }

}
