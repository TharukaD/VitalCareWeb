using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace VitalCareWeb.ViewModels.Brand
{
    public class AddBrandImageViewModel
    {
        public int BrandId { get; set; }

        [Required]
        [DisplayName("File")]
        public IFormFile UploadedFile { get; set; }
    }
}
