using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace VitalCareWeb.ViewModels.WhyChooseUsRecord
{
    public class AddWhyChooseUsRecordImageViewModel
    {
        public int RecordId { get; set; }

        [Required]
        [DisplayName("File")]
        public IFormFile UploadedFile { get; set; }
    }
}
