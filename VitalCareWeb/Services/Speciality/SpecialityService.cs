using Microsoft.EntityFrameworkCore;
using VitalCareWeb.Data;

namespace VitalCareWeb.Services.Speciality;

public class SpecialityService : ISpecialityService
{
    private ApplicationDbContext _context;
    public SpecialityService(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Entities.Speciality?> GetById(int id)
    {
        return await _context.FindAsync<Entities.Speciality>(id);
    }

    public async Task<IEnumerable<Entities.Speciality>> GetAll()
    {
        return await _context.Specilities.ToListAsync();
    }

    public async Task<bool> Add(Entities.Speciality speciality)
    {
        _context.Specilities.Add(speciality);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<(bool, string)> Delete(int id)
    {
        var speciality = await _context.Specilities.Include(r => r.Doctors).SingleOrDefaultAsync(r => r.Id == id);
        if (speciality != null)
        {
            if (speciality.Doctors.Any())
            {
                return (false, "Speciality already assigned with doctor");
            }

            _context.Specilities.Remove(speciality);
            await _context.SaveChangesAsync();
            return (true, "Success");
        }
        return (false, "Speciality not found");
    }

    public async Task<bool> IsDublicate(int id, string name)
    {
        if (id == 0)
        {
            if (await _context.Specilities.FirstOrDefaultAsync(r => r.Name == name) != null)
                return true;
        }
        else
        {
            if (await _context.Specilities.FirstOrDefaultAsync(r => r.Name == name && r.Id != id) != null)
                return true;
        }
        return false;
    }

    public async Task<bool> Update(Entities.Speciality speciality)
    {
        _context.Update(speciality);
        await _context.SaveChangesAsync();
        return true;
    }
}
