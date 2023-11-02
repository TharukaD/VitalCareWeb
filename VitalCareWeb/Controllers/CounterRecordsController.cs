using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VitalCareWeb.Entities;
using VitalCareWeb.Services.CounterRecord;
using VitalCareWeb.ViewModels;
using VitalCareWeb.ViewModels.CounterRecord;

namespace VitalCareWeb.Controllers
{
    public class CounterRecordsController : Controller
    {
        private IMapper _mapper;
        private ICounterRecordService _counterRecordService;
        private readonly ILogger<CounterRecordsController> _logger;

        public CounterRecordsController(
            IMapper mapper,
            ILogger<CounterRecordsController> logger,
            ICounterRecordService counterRecordService
            )
        {
            _mapper = mapper;
            _logger = logger;
            _counterRecordService = counterRecordService;
        }

        #region Index
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<CounterRecordViewModel>? output = new();

            var records = await _counterRecordService.GetAll();
            output = _mapper.Map<List<CounterRecordViewModel>>(records);

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
        public async Task<IActionResult> Add(AddEditCounterRecordViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Validations failed."));
                }

                if (await _counterRecordService.IsDublicate(0, viewModel.Name.Trim()) == true)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Name already exist"));
                }

                var record = _mapper.Map<AddEditCounterRecordViewModel, CounterRecord>(viewModel);

                await _counterRecordService.Add(record);
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
                var record = await _counterRecordService.GetById(id);

                if (record == null)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Record not found."));
                }

                var viewModel = _mapper.Map<AddEditCounterRecordViewModel>(record);
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
        public async Task<IActionResult> Edit(AddEditCounterRecordViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Validations failed."));
                }

                if (await _counterRecordService.IsDublicate(viewModel.Id.Value, viewModel.Name.Trim()) == true)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Name already exist"));
                }

                var record = _mapper.Map<AddEditCounterRecordViewModel, CounterRecord>(viewModel);

                await _counterRecordService.Update(record);
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
            var record = await _counterRecordService.GetById(id);
            if (record == null)
            {
                return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Record not found."));
            }
            var viewModel = _mapper.Map<CounterRecordViewModel>(record);
            return PartialView("_Delete", viewModel);
        }
        #endregion

        #region Delete [ HttpPost ]
        [HttpPost]
        public async Task<IActionResult> Delete(CounterRecordViewModel viewModel)
        {
            try
            {
                var result = await _counterRecordService.Delete(viewModel.Id);
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


        #region Upload Image [ HttpGet ]
        [HttpGet]
        public IActionResult UploadCounterRecordImage(int id)
        {
            var viewModel = new AddCounterRecordImageViewModel();
            viewModel.RecordId = id;
            return PartialView("_UploadCounterRecordImage", viewModel);
        }
        #endregion

        #region Upload Image [ HttpPost ]
        [HttpPost]
        public async Task<IActionResult> UploadCounterRecordImage(AddCounterRecordImageViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Validations failed."));
                }

                var record = await _counterRecordService.GetById(viewModel.RecordId);
                if (record == null)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Record not found."));
                }

                var file = viewModel.UploadedFile;

                var extension = file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                string filename = DateTime.Now.Ticks.ToString() + "." + extension;

                var filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\CounterRecordImages");

                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }

                var exactpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\CounterRecordImages", filename);
                using (var stream = new FileStream(exactpath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                record.Image = filename;
                await _counterRecordService.Update(record);
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
