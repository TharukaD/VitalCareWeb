using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace VitalCareWeb.ViewModels.Location
{
    public class AddEditLocationViewModel
    {
        [HiddenInput]
        public int? Id { get; set; }

        [HiddenInput]
        public string Image { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Short Description")]
        public string ShortDescription { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Phone No")]
        public string PhoneNo { get; set; }

        [Required]
        [Display(Name = "Viber / WhatsApp")]
        public string ViberWhatsupNo { get; set; }

        [Required]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Display(Name = "Facebook URL")]
        public string FacebookURL { get; set; }

        [Display(Name = "Instagram URL")]
        public string InstagramURL { get; set; }

        [Required]
        [Display(Name = "IFrame URL")]
        public string IFrameURL { get; set; }

        public int Priority { get; set; }
    }
}
