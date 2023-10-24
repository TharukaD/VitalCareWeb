using VitalCareWeb.Data;

namespace VitalCareWeb.Services.AppointmentReason;

using Microsoft.EntityFrameworkCore;
using VitalCareWeb.Entities;
public class AppointmentReasonService : IAppointmentReasonService
{
    private ApplicationDbContext _context;
    public AppointmentReasonService(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<AppointmentReason?> GetById(int id)
    {
        return await _context.FindAsync<AppointmentReason>(id);
    }

    public async Task<IEnumerable<AppointmentReason>> GetAll()
    {
        return await _context.AppointmentReasons.OrderBy(r => r.Priority).ToListAsync();
    }

    public async Task<bool> Add(AppointmentReason AppointmentReason)
    {
        _context.AppointmentReasons.Add(AppointmentReason);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Delete(int id)
    {
        var AppointmentReason = await _context.AppointmentReasons.SingleOrDefaultAsync(r => r.Id == id);
        if (AppointmentReason != null)
        {
            _context.AppointmentReasons.Remove(AppointmentReason);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<bool> IsDublicate(int id, string name)
    {
        if (id == 0)
        {
            if (await _context.AppointmentReasons.FirstOrDefaultAsync(r => r.Name == name) != null)
                return true;
        }
        else
        {
            if (await _context.AppointmentReasons.FirstOrDefaultAsync(r => r.Name == name && r.Id != id) != null)
                return true;
        }
        return false;
    }

    public async Task<bool> Update(AppointmentReason AppointmentReason)
    {
        _context.Update(AppointmentReason);
        await _context.SaveChangesAsync();
        return true;
    }
}
