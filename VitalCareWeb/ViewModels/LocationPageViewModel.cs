using VitalCareWeb.ViewModels.Doctor;
using VitalCareWeb.ViewModels.Service;

namespace VitalCareWeb.ViewModels
{
    public class LocationPageViewModel
    {
        public int LocationId { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string ImageUrl { get; set; }

        public List<DoctorViewModel> Doctors { get; set; }
        public List<ServiceViewModel> Services { get; set; }
        public ContactUsTabViewModel ContactRecord { get; set; }

        public void Initialize(List<DoctorViewModel> doctors, List<ServiceViewModel> services, ContactUsTabViewModel contactRecord)
        {
            Doctors = doctors;
            Services = services;
            ContactRecord = contactRecord;
        }
    }

}
