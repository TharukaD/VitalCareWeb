using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VitalCareWeb.Entities;
using VitalCareWeb.Services.Brand;
using VitalCareWeb.Services.Product;
using VitalCareWeb.ViewModels;
using VitalCareWeb.ViewModels.Brand;
using VitalCareWeb.ViewModels.Product;

namespace VitalCareWeb.Controllers
{
    public class ProductsController : Controller
    {
        private IMapper _mapper;
        private IProductService _productService;
        private IBrandService _brandService;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(
            IMapper mapper,
            ILogger<ProductsController> logger,
            IProductService productService,
            IBrandService brandService
            )
        {
            _mapper = mapper;
            _logger = logger;
            _productService = productService;
            _brandService = brandService;
        }

        #region Index
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<ProductViewModel>? output = new();

            var products = await _productService.GetAll();
            output = _mapper.Map<List<ProductViewModel>>(products);

            return View(output);
        }
        #endregion


        #region Add [ HttpGet ]
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var viewModel = new AddEditProductViewModel();

            var brandList = _mapper.Map<IList<BrandViewModel>>(await _brandService.GetAll());

            viewModel.Initialize(brandList.ToList());
            return PartialView("_AddEdit", viewModel);
        }
        #endregion

        #region Add [ HttpPost ]
        [HttpPost]
        public async Task<IActionResult> Add(AddEditProductViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Validations failed."));
                }

                if (await _productService.IsDublicate(0, viewModel.Name.Trim()) == true)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Product name already exist"));
                }

                var product = _mapper.Map<AddEditProductViewModel, Product>(viewModel);

                await _productService.Add(product);
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
                var brandList = _mapper.Map<IList<BrandViewModel>>(await _brandService.GetAll());

                var product = await _productService.GetById(id);

                if (product == null)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Product not found."));
                }

                var viewModel = _mapper.Map<AddEditProductViewModel>(product);
                viewModel.Initialize(brandList.ToList());

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
        public async Task<IActionResult> Edit(AddEditProductViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Validations failed."));
                }

                if (await _productService.IsDublicate(viewModel.Id.Value, viewModel.Name.Trim()) == true)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Product name already exist"));
                }

                var product = _mapper.Map<AddEditProductViewModel, Product>(viewModel);

                await _productService.Update(product);
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
            var product = await _productService.GetById(id);
            if (product == null)
            {
                return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Product not found."));
            }
            var viewModel = _mapper.Map<ProductViewModel>(product);
            return PartialView("_Delete", viewModel);
        }
        #endregion

        #region Delete [ HttpPost ]
        [HttpPost]
        public async Task<IActionResult> Delete(ProductViewModel viewModel)
        {
            try
            {
                var result = await _productService.Delete(viewModel.Id);
                if (result == true)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(true, "Successfully deleted.", "", true));
                }
                return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Failed to delete."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Failed to delete."));
            }
        }
        #endregion


        #region Upload Product Image [ HttpGet ]
        [HttpGet]
        public IActionResult UploadProductImage(int id)
        {
            var viewModel = new AddProductImageViewModel();
            viewModel.ProductId = id;
            return PartialView("_UploadProductImage", viewModel);
        }
        #endregion

        #region Upload Product Image [ HttpPost ]
        [HttpPost]
        public async Task<IActionResult> UploadProductImage(AddProductImageViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Validations failed."));
                }

                var product = await _productService.GetById(viewModel.ProductId);
                if (product == null)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Product not found."));
                }

                var file = viewModel.UploadedFile;

                var extension = file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                string filename = DateTime.Now.Ticks.ToString() + "." + extension;

                var filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\ProductImages");

                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }

                var exactpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\ProductImages", filename);
                using (var stream = new FileStream(exactpath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                product.Image = filename;
                await _productService.Update(product);
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
