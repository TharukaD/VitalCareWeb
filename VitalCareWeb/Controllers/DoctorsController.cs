using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VitalCareWeb.Entities;
using VitalCareWeb.Services.Doctor;
using VitalCareWeb.Services.Location;
using VitalCareWeb.Services.Speciality;
using VitalCareWeb.ViewModels;
using VitalCareWeb.ViewModels.Doctor;
using VitalCareWeb.ViewModels.Location;
using VitalCareWeb.ViewModels.Speciality;

namespace VitalCareWeb.Controllers;

[Authorize]
public class DoctorsController : Controller
{
    private IMapper _mapper;
    private IDoctorService _doctorService;
    private ISpecialityService _specialityService;
    private ILocationService _locationService;
    private readonly ILogger<DoctorsController> _logger;

    public DoctorsController(
        IMapper mapper,
        ILogger<DoctorsController> logger,
        IDoctorService doctorService,
        ISpecialityService specialityService,
        ILocationService locationService
        )
    {
        _mapper = mapper;
        _logger = logger;
        _doctorService = doctorService;
        _specialityService = specialityService;
        _locationService = locationService;
    }

    #region Index
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        List<DoctorViewModel>? output = new();

        var services = await _doctorService.GetAll();
        output = _mapper.Map<List<DoctorViewModel>>(services);

        return View(output);
    }
    #endregion


    #region Add [ HttpGet ]
    [HttpGet]
    public async Task<IActionResult> Add()
    {
        var viewModel = new AddEditDoctorViewModel();

        var specialityList = _mapper.Map<IList<SpecialityViewModel>>(await _specialityService.GetAll());
        var locationList = _mapper.Map<IList<LocationViewModel>>(await _locationService.GetAll());

        viewModel.Initialize(locationList.ToList(), specialityList.ToList());

        return PartialView("_AddEdit", viewModel);
    }
    #endregion

    #region Add [ HttpPost ]
    [HttpPost]
    public async Task<IActionResult> Add(AddEditDoctorViewModel viewModel)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Validations failed."));
            }

            if (await _doctorService.IsDublicate(0, viewModel.Name.Trim()) == true)
            {
                return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Doctor name already exist"));
            }

            var doctor = _mapper.Map<AddEditDoctorViewModel, Doctor>(viewModel);

            await _doctorService.Add(doctor);
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
            var doctor = await _doctorService.GetById(id);

            if (doctor == null)
            {
                return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Location not found."));
            }

            var viewModel = _mapper.Map<AddEditDoctorViewModel>(doctor);

            var specialityList = _mapper.Map<IList<SpecialityViewModel>>(await _specialityService.GetAll());
            var locationList = _mapper.Map<IList<LocationViewModel>>(await _locationService.GetAll());

            viewModel.Initialize(locationList.ToList(), specialityList.ToList());

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
    public async Task<IActionResult> Edit(AddEditDoctorViewModel viewModel)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Validations failed."));
            }

            if (await _doctorService.IsDublicate(viewModel.Id.Value, viewModel.Name.Trim()) == true)
            {
                return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Doctor name already exist"));
            }

            var doctor = _mapper.Map<AddEditDoctorViewModel, Doctor>(viewModel);

            await _doctorService.Update(doctor);
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
        var doctor = await _doctorService.GetById(id);
        if (doctor == null)
        {
            return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Doctor not found."));
        }
        var viewModel = _mapper.Map<DoctorViewModel>(doctor);
        return PartialView("_Delete", viewModel);
    }
    #endregion

    #region Delete [ HttpPost ]
    [HttpPost]
    public async Task<IActionResult> Delete(DoctorViewModel viewModel)
    {
        try
        {
            var result = await _doctorService.Delete(viewModel.Id);
            if (result.Item1 == true)
            {
                return PartialView("_AjaxActionResult", new AjaxActionResult(true, "Successfully deleted.", "", true));
            }
            return PartialView("_AjaxActionResult", new AjaxActionResult(false, $"Failed to delete.[ {result.Item2} ]"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Failed to delete."));
        }
    }
    #endregion


    #region Upload Doctor Image [ HttpGet ]
    [HttpGet]
    public IActionResult UploadDoctorImage(int id)
    {
        var viewModel = new AddDoctorImageViewModel();
        viewModel.DoctorId = id;
        return PartialView("_UploadDoctorImage", viewModel);
    }
    #endregion

    #region Upload Doctor Image [ HttpPost ]
    [HttpPost]
    public async Task<IActionResult> UploadDoctorImage(AddDoctorImageViewModel viewModel)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Validations failed."));
            }

            var doctor = await _doctorService.GetById(viewModel.DoctorId);
            if (doctor == null)
            {
                return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Doctor not found."));
            }

            var file = viewModel.UploadedFile;

            var extension = file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
            string filename = DateTime.Now.Ticks.ToString() + "." + extension;

            var filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\DoctorImages");

            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }

            var exactpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\DoctorImages", filename);
            using (var stream = new FileStream(exactpath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            doctor.Image = filename;
            await _doctorService.Update(doctor);
            return PartialView("_AjaxActionResult", new AjaxActionResult(true, "Successfully uploaded.", "", true));
        }
        catch (Exception ex)
        {
            return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Failed to upload."));
        }
    }
    #endregion
}
