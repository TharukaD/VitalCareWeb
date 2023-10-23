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

        public int Priority { get; set; }
    }
}
