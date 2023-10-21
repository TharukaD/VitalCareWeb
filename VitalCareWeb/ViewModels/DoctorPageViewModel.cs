using VitalCareWeb.ViewModels.Doctor;
using VitalCareWeb.ViewModels.Location;
using VitalCareWeb.ViewModels.Speciality;

namespace VitalCareWeb.ViewModels;

public class DoctorPageViewModel
{
    public List<DoctorViewModel> Doctors { get; set; }
    public List<SpecialityViewModel> Specialities { get; set; }
    public List<LocationViewModel> Locations { get; set; }

    public DoctorPageViewModel()
    {
        Doctors = new List<DoctorViewModel>();
        Specialities = new List<SpecialityViewModel>();
        Locations = new List<LocationViewModel>();
    }
}
