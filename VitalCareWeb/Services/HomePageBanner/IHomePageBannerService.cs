namespace VitalCareWeb.Services.HomePageBanner;
using VitalCareWeb.Entities;
public interface IHomePageBannerService
{
    Task<bool> IsDublicate(int id, string name);
    Task<HomePageBanner?> GetById(int id);
    Task<IEnumerable<HomePageBanner>> GetAll();
    Task<bool> Add(HomePageBanner homePageBanner);
    Task<bool> Update(HomePageBanner homePageBanner);
    Task<bool> Delete(int id);
}
