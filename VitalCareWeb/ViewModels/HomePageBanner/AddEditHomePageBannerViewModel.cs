using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace VitalCareWeb.ViewModels.HomePageBanner
{
    public class AddEditHomePageBannerViewModel
    {
        [HiddenInput]
        public int? Id { get; set; }

        [HiddenInput]
        public string SmallImage { get; set; }

        [HiddenInput]
        public string LargeImage { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Text Color")]
        public string TextColor { get; set; }

        public int Priority { get; set; }
    }
}
