using VitalCareWeb.ViewModels.Article;
using VitalCareWeb.ViewModels.Doctor;
using VitalCareWeb.ViewModels.Location;
using VitalCareWeb.ViewModels.WhyChooseUsRecord;

namespace VitalCareWeb.ViewModels;

public class HomePageViewModel
{
    public List<ServiceGroupViewModel> ServiceGroups { get; set; }
    public List<DoctorViewModel> Doctors { get; set; }
    public List<ArticleViewModel> Articles { get; set; }
    public List<LocationViewModel> Locations { get; set; }
    public List<WhyChooseUsRecordViewModel> WhyChooseUsRecords { get; set; }

    public HomePageViewModel()
    {
        Doctors = new List<DoctorViewModel>();
        Articles = new List<ArticleViewModel>();
        ServiceGroups = new List<ServiceGroupViewModel>();
        Locations = new List<LocationViewModel>();
        WhyChooseUsRecords = new List<WhyChooseUsRecordViewModel>();
    }
}
