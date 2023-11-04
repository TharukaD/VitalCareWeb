using VitalCareWeb.Data;
namespace VitalCareWeb.Services.Brand;

using Microsoft.EntityFrameworkCore;
using VitalCareWeb.Entities;

public class BrandService : IBrandService
{
    private ApplicationDbContext _context;
    public BrandService(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Brand?> GetById(int id)
    {
        return await _context.FindAsync<Brand>(id);
    }

    public async Task<IEnumerable<Brand>> GetAll()
    {
        return await _context.Brands.Include(r => r.Products).OrderBy(r => r.Priority).ToListAsync();
    }

    public async Task<bool> Add(Brand brand)
    {
        _context.Brands.Add(brand);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<(bool, string)> Delete(int id)
    {
        var brand = await _context.Brands.Include(r => r.Products).SingleOrDefaultAsync(r => r.Id == id);
        if (brand != null)
        {
            if (brand.Products.Any())
            {
                return (false, "Brand already assigned with product");
            }

            _context.Brands.Remove(brand);
            await _context.SaveChangesAsync();
            return (true, "Success");
        }
        return (false, "Brand not found");
    }

    public async Task<bool> IsDublicate(int id, string name)
    {
        if (id == 0)
        {
            if (await _context.Brands.FirstOrDefaultAsync(r => r.Name == name) != null)
                return true;
        }
        else
        {
            if (await _context.Brands.FirstOrDefaultAsync(r => r.Name == name && r.Id != id) != null)
                return true;
        }
        return false;
    }

    public async Task<bool> Update(Brand brand)
    {
        _context.Update(brand);
        await _context.SaveChangesAsync();
        return true;
    }
}
