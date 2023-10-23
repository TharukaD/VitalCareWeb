namespace VitalCareWeb.ViewModels;
using VitalCareWeb.ViewModels.Service;

public class ServiceGroupViewModel
{
    public int LocationId { get; set; }
    public string LocationName { get; set; }
    public List<ServiceViewModel> Services { get; set; }


    public string TabId { get; set; }
    public string TargetId { get; set; }
    public string AreaControlId { get; set; }
    public string TabHeaderActiveClass { get; set; }
    public string TabContentActiveClass { get; set; }
}
