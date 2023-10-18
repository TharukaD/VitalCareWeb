using VitalCareWeb.ViewModels.Doctor;
using VitalCareWeb.ViewModels.Speciality;

namespace VitalCareWeb.ViewModels;

public class DoctorPageViewModel
{
    public List<DoctorViewModel> Doctors { get; set; }
    public List<SpecialityViewModel> Specialities { get; set; }

    public DoctorPageViewModel()
    {
        Doctors = new List<DoctorViewModel>();
        Specialities = new List<SpecialityViewModel>();
    }
}
