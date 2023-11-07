using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VitalCareWeb.Entities;
using VitalCareWeb.Services.WhyChooseUsRecord;
using VitalCareWeb.ViewModels;
using VitalCareWeb.ViewModels.WhyChooseUsRecord;

namespace VitalCareWeb.Controllers
{
    [Authorize]
    public class WhyChooseUsRecordsController : Controller
    {
        private IMapper _mapper;
        private IWhyChooseUsRecordService _whyChooseUsRecordService;
        private readonly ILogger<WhyChooseUsRecordsController> _logger;

        public WhyChooseUsRecordsController(
            IMapper mapper,
            ILogger<WhyChooseUsRecordsController> logger,
            IWhyChooseUsRecordService whyChooseUsRecordService
            )
        {
            _mapper = mapper;
            _logger = logger;
            _whyChooseUsRecordService = whyChooseUsRecordService;
        }

        #region Index
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<WhyChooseUsRecordViewModel>? output = new();

            var records = await _whyChooseUsRecordService.GetAll();
            output = _mapper.Map<List<WhyChooseUsRecordViewModel>>(records);

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
        public async Task<IActionResult> Add(AddEditWhyChooseUsRecordViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Validations failed."));
                }

                if (await _whyChooseUsRecordService.IsDublicate(0, viewModel.Name.Trim()) == true)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Name already exist"));
                }

                var record = _mapper.Map<AddEditWhyChooseUsRecordViewModel, WhyChooseUsRecord>(viewModel);

                await _whyChooseUsRecordService.Add(record);
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
                var record = await _whyChooseUsRecordService.GetById(id);

                if (record == null)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Record not found."));
                }

                var viewModel = _mapper.Map<AddEditWhyChooseUsRecordViewModel>(record);
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
        public async Task<IActionResult> Edit(AddEditWhyChooseUsRecordViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Validations failed."));
                }

                if (await _whyChooseUsRecordService.IsDublicate(viewModel.Id.Value, viewModel.Name.Trim()) == true)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Name already exist"));
                }

                var record = _mapper.Map<AddEditWhyChooseUsRecordViewModel, WhyChooseUsRecord>(viewModel);

                await _whyChooseUsRecordService.Update(record);
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
            var record = await _whyChooseUsRecordService.GetById(id);
            if (record == null)
            {
                return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Record not found."));
            }
            var viewModel = _mapper.Map<WhyChooseUsRecordViewModel>(record);
            return PartialView("_Delete", viewModel);
        }
        #endregion

        #region Delete [ HttpPost ]
        [HttpPost]
        public async Task<IActionResult> Delete(WhyChooseUsRecordViewModel viewModel)
        {
            try
            {
                var result = await _whyChooseUsRecordService.Delete(viewModel.Id);
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
        public IActionResult UploadWhyChooseUsRecordImage(int id)
        {
            var viewModel = new AddWhyChooseUsRecordImageViewModel();
            viewModel.RecordId = id;
            return PartialView("_UploadWhyChooseUsRecordImage", viewModel);
        }
        #endregion

        #region Upload Image [ HttpPost ]
        [HttpPost]
        public async Task<IActionResult> UploadWhyChooseUsRecordImage(AddWhyChooseUsRecordImageViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Validations failed."));
                }

                var record = await _whyChooseUsRecordService.GetById(viewModel.RecordId);
                if (record == null)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Record not found."));
                }

                var file = viewModel.UploadedFile;

                var extension = file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                string filename = DateTime.Now.Ticks.ToString() + "." + extension;

                var filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\WhyChooseUsRecordImages");

                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }

                var exactpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\WhyChooseUsRecordImages", filename);
                using (var stream = new FileStream(exactpath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                record.Image = filename;
                await _whyChooseUsRecordService.Update(record);
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
