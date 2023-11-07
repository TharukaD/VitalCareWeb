using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VitalCareWeb.Entities;
using VitalCareWeb.Services.HomePageBanner;
using VitalCareWeb.ViewModels;
using VitalCareWeb.ViewModels.HomePageBanner;

namespace VitalCareWeb.Controllers
{
    [Authorize]
    public class HomePageBannersController : Controller
    {
        private IMapper _mapper;
        private IHomePageBannerService _homePageBanner;
        private readonly ILogger<HomePageBannersController> _logger;

        public HomePageBannersController(
            IMapper mapper,
            ILogger<HomePageBannersController> logger,
            IHomePageBannerService homePageBanner
            )
        {
            _mapper = mapper;
            _logger = logger;
            _homePageBanner = homePageBanner;
        }

        #region Index
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<HomePageBannerViewModel>? output = new();

            var banners = await _homePageBanner.GetAll();
            output = _mapper.Map<List<HomePageBannerViewModel>>(banners);

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
        public async Task<IActionResult> Add(AddEditHomePageBannerViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Validations failed."));
                }

                if (await _homePageBanner.IsDublicate(0, viewModel.Title.Trim()) == true)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Title already exist"));
                }

                var banner = _mapper.Map<AddEditHomePageBannerViewModel, HomePageBanner>(viewModel);

                await _homePageBanner.Add(banner);
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
                var banner = await _homePageBanner.GetById(id);

                if (banner == null)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Banner not found."));
                }

                var viewModel = _mapper.Map<AddEditHomePageBannerViewModel>(banner);
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
        public async Task<IActionResult> Edit(AddEditHomePageBannerViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Validations failed."));
                }

                if (await _homePageBanner.IsDublicate(viewModel.Id.Value, viewModel.Title.Trim()) == true)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Title already exist"));
                }

                var banner = _mapper.Map<AddEditHomePageBannerViewModel, HomePageBanner>(viewModel);

                await _homePageBanner.Update(banner);
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
            var banner = await _homePageBanner.GetById(id);
            if (banner == null)
            {
                return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Banner not found."));
            }
            var viewModel = _mapper.Map<HomePageBannerViewModel>(banner);
            return PartialView("_Delete", viewModel);
        }
        #endregion

        #region Delete [ HttpPost ]
        [HttpPost]
        public async Task<IActionResult> Delete(HomePageBannerViewModel viewModel)
        {
            try
            {
                var result = await _homePageBanner.Delete(viewModel.Id);
                if (result == true)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(true, "Successfully deleted.", "", true));
                }
                return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Failed to delete"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Failed to delete."));
            }
        }
        #endregion


        #region Upload Banner Image [ HttpGet ]
        [HttpGet]
        public IActionResult UploadHomePageBannerImage(int id, string type)
        {
            var viewModel = new AddHomePageBannerImageViewModel();
            viewModel.BannerId = id;
            viewModel.Type = type;
            return PartialView("_UploadBannerImage", viewModel);
        }
        #endregion

        #region Upload Location Image [ HttpPost ]
        [HttpPost]
        public async Task<IActionResult> UploadHomePageBannerImage(AddHomePageBannerImageViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Validations failed."));
                }

                var banner = await _homePageBanner.GetById(viewModel.BannerId);
                if (banner == null)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Banner not found."));
                }

                var file = viewModel.UploadedFile;

                var extension = file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                string filename = DateTime.Now.Ticks.ToString() + "." + extension;

                if (viewModel.Type == "Small")
                {
                    var filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\HomePageBannerSmallImage");

                    if (!Directory.Exists(filepath))
                    {
                        Directory.CreateDirectory(filepath);
                    }

                    var exactpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\HomePageBannerSmallImage", filename);
                    using (var stream = new FileStream(exactpath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    banner.SmallImage = filename;
                }
                else
                {
                    var filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\HomePageBannerLargeImage");

                    if (!Directory.Exists(filepath))
                    {
                        Directory.CreateDirectory(filepath);
                    }

                    var exactpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\HomePageBannerLargeImage", filename);
                    using (var stream = new FileStream(exactpath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    banner.LargeImage = filename;
                }

                await _homePageBanner.Update(banner);
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
