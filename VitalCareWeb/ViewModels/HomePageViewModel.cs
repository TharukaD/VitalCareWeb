using VitalCareWeb.ViewModels.Article;
using VitalCareWeb.ViewModels.Brand;
using VitalCareWeb.ViewModels.CounterRecord;
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
    public List<CounterRecordViewModel> CounterRecords { get; set; }
    public List<BrandViewModel> Brands { get; set; }

    public HomePageViewModel()
    {
        Doctors = new List<DoctorViewModel>();
        Articles = new List<ArticleViewModel>();
        ServiceGroups = new List<ServiceGroupViewModel>();
        Locations = new List<LocationViewModel>();
        WhyChooseUsRecords = new List<WhyChooseUsRecordViewModel>();
        CounterRecords = new List<CounterRecordViewModel>();
        Brands = new List<BrandViewModel>();
    }
}
