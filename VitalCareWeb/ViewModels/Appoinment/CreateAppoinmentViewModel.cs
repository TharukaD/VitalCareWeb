using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
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
        public string ReasonForVisit { get; set; }

        [Required]
        [Display(Name = "Select Speciality")]
        public string SpecialityId { get; set; }
        public SelectList SpecialitySelectList { get; set; }


        public string DoctorId { get; set; }
        public SelectList DoctorSelectList { get; set; }
        public DateTime PreferredDate { get; set; }
        public DateTime PreferredTime { get; set; }

        public void Initialize(List<SpecialityViewModel> specilaityList, List<DoctorViewModel> doctorList)
        {
            SpecialitySelectList = new SelectList(specilaityList, "Id", "Name");
            DoctorSelectList = new SelectList(doctorList, "Id", "Name");
        }
    }

}
