using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace VitalCareWeb.ViewModels.Location
{
    public class AddEditLocationViewModel
    {
        [HiddenInput]
        public int? Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
