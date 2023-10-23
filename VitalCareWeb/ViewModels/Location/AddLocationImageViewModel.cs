using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace VitalCareWeb.ViewModels.Location
{
    public class AddLocationImageViewModel
    {
        public int LocationId { get; set; }

        [Required]
        [DisplayName("File")]
        public IFormFile UploadedFile { get; set; }
    }
}
