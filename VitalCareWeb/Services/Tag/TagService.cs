using Microsoft.EntityFrameworkCore;
using VitalCareWeb.Data;

namespace VitalCareWeb.Services.Tag;

public class TagService : ITagService
{
    private ApplicationDbContext _context;
    public TagService(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Entities.Tag?> GetById(int id)
    {
        return await _context.FindAsync<Entities.Tag>(id);
    }

    public async Task<IEnumerable<Entities.Tag>> GetAll()
    {
        return await _context.Tags.ToListAsync();
    }

    public async Task<bool> Add(Entities.Tag tag)
    {
        _context.Tags.Add(tag);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<(bool, string)> Delete(int id)
    {
        var tag = await _context.Tags.Include(r => r.ArticleTags).SingleOrDefaultAsync(r => r.Id == id);
        if (tag != null)
        {
            if (tag.ArticleTags.Any())
            {
                return (false, "Tag already assigned with article");
            }

            _context.Tags.Remove(tag);
            await _context.SaveChangesAsync();
            return (true, "Success");
        }
        return (false, "tag not found");
    }

    public async Task<bool> IsDublicate(int id, string name)
    {
        if (id == 0)
        {
            if (await _context.Tags.FirstOrDefaultAsync(r => r.Name == name) != null)
                return true;
        }
        else
        {
            if (await _context.Tags.FirstOrDefaultAsync(r => r.Name == name && r.Id != id) != null)
                return true;
        }
        return false;
    }

    public async Task<bool> Update(Entities.Tag tag)
    {
        _context.Update(tag);
        await _context.SaveChangesAsync();
        return true;
    }
}
