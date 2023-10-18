using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VitalCareWeb.Constants;
using VitalCareWeb.ViewModels.Location;
using VitalCareWeb.ViewModels.Speciality;

namespace VitalCareWeb.ViewModels.Doctor
{
    public class AddEditDoctorViewModel
    {
        [HiddenInput]
        public int? Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Gender { get; set; }
        public SelectList GenderSelectList { get; set; }

        [HiddenInput]
        public string Image { get; set; }

        [Required]
        public int LocationId { get; set; }
        public SelectList LocationSelectList { get; set; }

        [Required]
        public int SpecialityId { get; set; }
        public SelectList SpecialitySelectList { get; set; }

        [Required]
        [DisplayName("Qualifications")]
        public string Qualifications { get; set; }

        [Required]
        [DisplayName("Short Description")]
        public string ShortDescription { get; set; }

        [Required]
        [DisplayName("Long Description")]
        public string LongDescription { get; set; }


        [DisplayName("Facebook Url")]
        public string? FacebookUrl { get; set; }

        [DisplayName("Twitter Url")]
        public string? TwitterUrl { get; set; }

        [DisplayName("Instagram Url")]
        public string? InstagramUrl { get; set; }

        public void Initialize(List<LocationViewModel> locationList, List<SpecialityViewModel> specilaityList)
        {
            LocationSelectList = new SelectList(locationList, "Id", "Name");
            SpecialitySelectList = new SelectList(specilaityList, "Id", "Name");

            List<SelectListItem> genderSelectList = new List<SelectListItem>();
            genderSelectList.Add(new SelectListItem() { Text = ModelConstants.Gender.Male, Value = ModelConstants.Gender.Male });
            genderSelectList.Add(new SelectListItem() { Text = ModelConstants.Gender.Female, Value = ModelConstants.Gender.Female });
            GenderSelectList = new SelectList(genderSelectList, "Value", "Text");
        }
    }
}
