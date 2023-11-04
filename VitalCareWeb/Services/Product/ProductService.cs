using VitalCareWeb.Data;

namespace VitalCareWeb.Services.Product;

using Microsoft.EntityFrameworkCore;
using VitalCareWeb.Entities;
public class ProductService : IProductService
{
    private ApplicationDbContext _context;
    public ProductService(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Product?> GetById(int id)
    {
        return await _context.Products.Include(r => r.Brand).SingleOrDefaultAsync(r => r.Id == id);
    }

    public async Task<IEnumerable<Product>> GetAll()
    {
        return await _context.Products.Include(r => r.Brand).OrderBy(r => r.Priority).ToListAsync();
    }

    public async Task<bool> Add(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Delete(int id)
    {
        var product = await _context.Products.SingleOrDefaultAsync(r => r.Id == id);
        if (product != null)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<bool> IsDublicate(int id, string name)
    {
        if (id == 0)
        {
            if (await _context.Products.FirstOrDefaultAsync(r => r.Name == name) != null)
                return true;
        }
        else
        {
            if (await _context.Products.FirstOrDefaultAsync(r => r.Name == name && r.Id != id) != null)
                return true;
        }
        return false;
    }

    public async Task<bool> Update(Product product)
    {
        _context.Update(product);
        await _context.SaveChangesAsync();
        return true;
    }
}
