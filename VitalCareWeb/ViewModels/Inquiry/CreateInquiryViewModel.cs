using System.ComponentModel.DataAnnotations;

namespace VitalCareWeb.ViewModels.Inquiry
{
    public class CreateInquiryViewModel
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNo { get; set; }

        [Required]
        [Display(Name = "Message")]
        public string Message { get; set; }
    }
}
