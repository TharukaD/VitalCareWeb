using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VitalCareWeb.Services.Inquiry;
using VitalCareWeb.ViewModels.Inquiry;

namespace VitalCareWeb.Controllers
{
    public class InquiriesController : Controller
    {
        private IMapper _mapper;
        private IInquiryService _inquiryService;
        private readonly ILogger<InquiriesController> _logger;

        public InquiriesController(
           IMapper mapper,
           ILogger<InquiriesController> logger,
           IInquiryService inquiryService)
        {
            _mapper = mapper;
            _logger = logger;
            _inquiryService = inquiryService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<InquiryViewModel>? output = new();

            var inquiries = await _inquiryService.GetAll();
            output = _mapper.Map<List<InquiryViewModel>>(inquiries);

            return View(output);
        }
    }
}
