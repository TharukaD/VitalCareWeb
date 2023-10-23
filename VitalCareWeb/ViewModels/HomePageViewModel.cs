using VitalCareWeb.ViewModels.Article;
using VitalCareWeb.ViewModels.Doctor;

namespace VitalCareWeb.ViewModels;

public class HomePageViewModel
{
    public List<ServiceGroupViewModel> ServiceGroups { get; set; }
    public List<DoctorViewModel> Doctors { get; set; }
    public List<ArticleViewModel> Articles { get; set; }

    public HomePageViewModel()
    {
        Doctors = new List<DoctorViewModel>();
        Articles = new List<ArticleViewModel>();
        ServiceGroups = new List<ServiceGroupViewModel>();
    }
}
