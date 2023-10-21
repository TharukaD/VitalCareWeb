using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace VitalCareWeb.ViewModels.Tag
{
    public class AddEditTagViewModel
    {
        [HiddenInput]
        public int? Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
