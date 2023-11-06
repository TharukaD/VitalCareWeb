using VitalCareWeb.Data;

namespace VitalCareWeb.Services.HomePageBanner;

using Microsoft.EntityFrameworkCore;
using VitalCareWeb.Entities;

public class HomePageBannerService : IHomePageBannerService
{
    private ApplicationDbContext _context;
    public HomePageBannerService(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<HomePageBanner?> GetById(int id)
    {
        return await _context.FindAsync<HomePageBanner>(id);
    }

    public async Task<IEnumerable<HomePageBanner>> GetAll()
    {
        return await _context.HomePageBanners.OrderBy(r => r.Priority).ToListAsync();
    }

    public async Task<bool> Add(HomePageBanner banner)
    {
        _context.HomePageBanners.Add(banner);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Delete(int id)
    {
        var banner = await _context.HomePageBanners.SingleOrDefaultAsync(r => r.Id == id);
        if (banner != null)
        {
            _context.HomePageBanners.Remove(banner);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<bool> IsDublicate(int id, string name)
    {
        if (id == 0)
        {
            if (await _context.HomePageBanners.FirstOrDefaultAsync(r => r.Title == name) != null)
                return true;
        }
        else
        {
            if (await _context.HomePageBanners.FirstOrDefaultAsync(r => r.Title == name && r.Id != id) != null)
                return true;
        }
        return false;
    }

    public async Task<bool> Update(HomePageBanner banner)
    {
        _context.Update(banner);
        await _context.SaveChangesAsync();
        return true;
    }
}
