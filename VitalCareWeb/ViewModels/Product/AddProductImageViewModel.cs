using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace VitalCareWeb.ViewModels.Product
{
    public class AddProductImageViewModel
    {
        public int ProductId { get; set; }

        [Required]
        [DisplayName("File")]
        public IFormFile UploadedFile { get; set; }
    }
}
