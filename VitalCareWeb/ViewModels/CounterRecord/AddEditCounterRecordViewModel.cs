using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace VitalCareWeb.ViewModels.CounterRecord
{
    public class AddEditCounterRecordViewModel
    {
        [HiddenInput]
        public int? Id { get; set; }

        [HiddenInput]
        public string Image { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Count Info")]
        public string CountInfo { get; set; }

        public int? Priority { get; set; }
    }
}
