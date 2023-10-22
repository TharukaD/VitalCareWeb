using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VitalCareWeb.ViewModels.Location;

namespace VitalCareWeb.ViewModels.Service
{
    public class AddEditServiceViewModel
    {
        [HiddenInput]
        public int? Id { get; set; }

        [Required]
        [DisplayName("Name")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Short Description")]
        public string ShortDescription { get; set; }

        [Required]
        [DisplayName("Long Description")]
        public string LongDescription { get; set; }

        [Required]
        [DisplayName("Priority")]
        public int Priority { get; set; }

        public string? Image { get; set; }

        [Required]
        [DisplayName("Location")]
        public int LocationId { get; set; }
        public SelectList LocationSelectList { get; set; }

        public void Initialize(List<LocationViewModel> locationList)
        {
            LocationSelectList = new SelectList(locationList, "Id", "Name");
        }

    }
}
