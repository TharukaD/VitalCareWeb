using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace VitalCareWeb.ViewModels.AppointmentReason
{
    public class AddEditAppointmentReasonViewModel
    {
        [HiddenInput]
        public int? Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Priority { get; set; }
    }
}
