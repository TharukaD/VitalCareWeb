using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using VitalCareWeb.ViewModels.AppointmentReason;
using VitalCareWeb.ViewModels.Doctor;
using VitalCareWeb.ViewModels.Speciality;

namespace VitalCareWeb.ViewModels.Appoinment
{
    public class CreateAppoinmentViewModel
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNo { get; set; }

        [Required]
        [Display(Name = "ID Card / Passport No")]
        public string IdentityNo { get; set; }

        [Required]
        [Display(Name = "Reason for visit")]
        public int ReasonId { get; set; }
        public SelectList ReasonSelectList { get; set; }

        [Required]
        [Display(Name = "Select Speciality")]
        public int SpecialityId { get; set; }
        public SelectList SpecialitySelectList { get; set; }

        [Required]
        [Display(Name = "Select Doctor")]
        public int DoctorId { get; set; }
        public SelectList DoctorSelectList { get; set; }

        [Required]
        [Display(Name = "Preferred Date")]
        public DateTime PreferredDate { get; set; }

        [Required]
        [Display(Name = "Preferred Time")]
        [DisplayFormat(DataFormatString = "{0:hh:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime PreferredTime { get; set; }

        public void Initialize(List<AppointmentReasonViewModel> reasonList, List<SpecialityViewModel> specilaityList, List<DoctorViewModel> doctorList)
        {
            ReasonSelectList = new SelectList(reasonList, "Id", "Name");
            SpecialitySelectList = new SelectList(specilaityList, "Id", "Name");
            DoctorSelectList = new SelectList(doctorList, "Id", "Name");
            PreferredDate = DateTime.Now;
        }
    }

}
