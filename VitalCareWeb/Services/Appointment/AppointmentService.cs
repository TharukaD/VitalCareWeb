using VitalCareWeb.Data;

namespace VitalCareWeb.Services.Appointment;

using Microsoft.EntityFrameworkCore;
using VitalCareWeb.Entities;

public class AppointmentService : IAppointmentService
{
    private ApplicationDbContext _context;
    public AppointmentService(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Appointment?> GetById(int id)
    {
        return await _context.FindAsync<Appointment>(id);
    }

    public async Task<IEnumerable<Appointment>> GetAll()
    {
        return await _context.Appointments.OrderBy(r => r.CreatedDate).ToListAsync();
    }

    public async Task<bool> Add(Appointment appointment)
    {
        _context.Appointments.Add(appointment);
        await _context.SaveChangesAsync();
        return true;
    }
}

