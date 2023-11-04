using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VitalCareWeb.Entities;
using VitalCareWeb.Services.Brand;
using VitalCareWeb.ViewModels;
using VitalCareWeb.ViewModels.Brand;

namespace VitalCareWeb.Controllers
{
    public class BrandsController : Controller
    {
        private IMapper _mapper;
        private IBrandService _brandService;
        private readonly ILogger<LocationsController> _logger;

        public BrandsController(
            IMapper mapper,
            ILogger<LocationsController> logger,
            IBrandService brandService
            )
        {
            _mapper = mapper;
            _logger = logger;
            _brandService = brandService;
        }

        #region Index
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<BrandViewModel>? output = new();

            var brands = await _brandService.GetAll();
            output = _mapper.Map<List<BrandViewModel>>(brands);

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
        public async Task<IActionResult> Add(AddEditBrandViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Validations failed."));
                }

                if (await _brandService.IsDublicate(0, viewModel.Name.Trim()) == true)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Brand name already exist"));
                }

                var brand = _mapper.Map<AddEditBrandViewModel, Brand>(viewModel);

                await _brandService.Add(brand);
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
                var brand = await _brandService.GetById(id);

                if (brand == null)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Brand not found."));
                }

                var viewModel = _mapper.Map<AddEditBrandViewModel>(brand);
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
        public async Task<IActionResult> Edit(AddEditBrandViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Validations failed."));
                }

                if (await _brandService.IsDublicate(viewModel.Id.Value, viewModel.Name.Trim()) == true)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Brand name already exist"));
                }

                var brand = _mapper.Map<AddEditBrandViewModel, Brand>(viewModel);

                await _brandService.Update(brand);
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
            var brand = await _brandService.GetById(id);
            if (brand == null)
            {
                return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Brand not found."));
            }
            var viewModel = _mapper.Map<BrandViewModel>(brand);
            return PartialView("_Delete", viewModel);
        }
        #endregion

        #region Delete [ HttpPost ]
        [HttpPost]
        public async Task<IActionResult> Delete(BrandViewModel viewModel)
        {
            try
            {
                var result = await _brandService.Delete(viewModel.Id);
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


        #region Upload Brand Image [ HttpGet ]
        [HttpGet]
        public IActionResult UploadBrandImage(int id)
        {
            var viewModel = new AddBrandImageViewModel();
            viewModel.BrandId = id;
            return PartialView("_UploadBrandImage", viewModel);
        }
        #endregion

        #region Upload Brand Image [ HttpPost ]
        [HttpPost]
        public async Task<IActionResult> UploadBrandImage(AddBrandImageViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Validations failed."));
                }

                var brand = await _brandService.GetById(viewModel.BrandId);
                if (brand == null)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Brand not found."));
                }

                var file = viewModel.UploadedFile;

                var extension = file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                string filename = DateTime.Now.Ticks.ToString() + "." + extension;

                var filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\BrandImages");

                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }

                var exactpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\BrandImages", filename);
                using (var stream = new FileStream(exactpath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                brand.Image = filename;
                await _brandService.Update(brand);
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
