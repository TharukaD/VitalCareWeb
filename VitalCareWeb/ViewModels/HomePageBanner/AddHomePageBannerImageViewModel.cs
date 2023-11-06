using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace VitalCareWeb.ViewModels.HomePageBanner
{
    public class AddHomePageBannerImageViewModel
    {
        public int BannerId { get; set; }
        public string Type { get; set; }

        [Required]
        [DisplayName("File")]
        public IFormFile UploadedFile { get; set; }
    }
}
