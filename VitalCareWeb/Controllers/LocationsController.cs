﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VitalCareWeb.Entities;
using VitalCareWeb.Services.Location;
using VitalCareWeb.ViewModels;
using VitalCareWeb.ViewModels.Location;

namespace VitalCareWeb.Controllers
{
    [Authorize]
    public class LocationsController : Controller
    {
        private IMapper _mapper;
        private ILocationService _locationService;
        private readonly ILogger<LocationsController> _logger;

        public LocationsController(
            IMapper mapper,
            ILogger<LocationsController> logger,
            ILocationService locationService
            )
        {
            _mapper = mapper;
            _logger = logger;
            _locationService = locationService;
        }

        #region Index
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<LocationViewModel>? output = new();

            var services = await _locationService.GetAll();
            output = _mapper.Map<List<LocationViewModel>>(services);

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
        public async Task<IActionResult> Add(AddEditLocationViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Validations failed."));
                }

                if (await _locationService.IsDublicate(0, viewModel.Name.Trim()) == true)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Location name already exist"));
                }

                var location = _mapper.Map<AddEditLocationViewModel, Location>(viewModel);

                await _locationService.Add(location);
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
                var location = await _locationService.GetById(id);

                if (location == null)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Location not found."));
                }

                var viewModel = _mapper.Map<AddEditLocationViewModel>(location);
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
        public async Task<IActionResult> Edit(AddEditLocationViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Validations failed."));
                }

                if (await _locationService.IsDublicate(viewModel.Id.Value, viewModel.Name.Trim()) == true)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Location name already exist"));
                }

                var location = _mapper.Map<AddEditLocationViewModel, Location>(viewModel);

                await _locationService.Update(location);
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
            var location = await _locationService.GetById(id);
            if (location == null)
            {
                return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Location not found."));
            }
            var viewModel = _mapper.Map<LocationViewModel>(location);
            return PartialView("_Delete", viewModel);
        }
        #endregion

        #region Delete [ HttpPost ]
        [HttpPost]
        public async Task<IActionResult> Delete(LocationViewModel viewModel)
        {
            try
            {
                var result = await _locationService.Delete(viewModel.Id);
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


        #region Upload Location Image [ HttpGet ]
        [HttpGet]
        public IActionResult UploadLocationImage(int id)
        {
            var viewModel = new AddLocationImageViewModel();
            viewModel.LocationId = id;
            return PartialView("_UploadLocationImage", viewModel);
        }
        #endregion

        #region Upload Location Image [ HttpPost ]
        [HttpPost]
        public async Task<IActionResult> UploadLocationImage(AddLocationImageViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Validations failed."));
                }

                var location = await _locationService.GetById(viewModel.LocationId);
                if (location == null)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Location not found."));
                }

                var file = viewModel.UploadedFile;

                var extension = file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                string filename = DateTime.Now.Ticks.ToString() + "." + extension;

                var filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\LocationImages");

                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }

                var exactpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\LocationImages", filename);
                using (var stream = new FileStream(exactpath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                location.Image = filename;
                await _locationService.Update(location);
                return PartialView("_AjaxActionResult", new AjaxActionResult(true, "Successfully uploaded.", "", true));
            }
            catch (Exception ex)
            {
                return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Failed to upload."));
            }
        }
        #endregion
    }
}
