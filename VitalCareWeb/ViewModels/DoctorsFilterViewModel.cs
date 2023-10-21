namespace VitalCareWeb.ViewModels
{
    public class DoctorsFilterViewModel
    {
        public string? DoctorName { get; set; }
        public List<string>? Genders { get; set; }
        public List<int>? Specialities { get; set; }
        public List<int>? Locations { get; set; }
    }
}
