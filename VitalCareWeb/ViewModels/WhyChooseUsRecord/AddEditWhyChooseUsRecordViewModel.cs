using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace VitalCareWeb.ViewModels.WhyChooseUsRecord
{
    public class AddEditWhyChooseUsRecordViewModel
    {
        [HiddenInput]
        public int? Id { get; set; }

        [HiddenInput]
        public string Image { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

        public int Priority { get; set; }
    }
}
