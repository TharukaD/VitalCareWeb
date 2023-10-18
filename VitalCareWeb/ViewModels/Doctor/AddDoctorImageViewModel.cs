using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace VitalCareWeb.ViewModels.Doctor
{
    public class AddDoctorImageViewModel
    {
        public int DoctorId { get; set; }

        [Required]
        [DisplayName("File")]
        public IFormFile UploadedFile { get; set; }
    }
}
