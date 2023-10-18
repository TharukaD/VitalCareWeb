using Microsoft.EntityFrameworkCore;
using VitalCareWeb.Data;

namespace VitalCareWeb.Services.Doctor
{
    public class DoctorService : IDoctorService
    {
        private ApplicationDbContext _context;
        public DoctorService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Entities.Doctor?> GetById(int id)
        {
            return await _context.Doctors
                .Include(d => d.Location)
                .Include(d => d.Speciality)
                .SingleOrDefaultAsync(d => d.Id == id);
        }

        public async Task<IEnumerable<Entities.Doctor>> GetAll()
        {
            return await _context.Doctors
                .Include(d => d.Location)
                .Include(d => d.Speciality)
                .ToListAsync();
        }

        public async Task<bool> Add(Entities.Doctor Doctor)
        {
            _context.Doctors.Add(Doctor);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<(bool, string)> Delete(int id)
        {
            var Doctor = await _context.Doctors.FindAsync(id);
            if (Doctor != null)
            {
                _context.Doctors.Remove(Doctor);
                await _context.SaveChangesAsync();
                return (true, "Success");
            }
            return (false, "Failed to delete");
        }

        public async Task<bool> IsDublicate(int id, string name)
        {
            if (id == 0)
            {
                if (await _context.Doctors.FirstOrDefaultAsync(r => r.Name == name) != null)
                    return true;
            }
            else
            {
                if (await _context.Doctors.FirstOrDefaultAsync(r => r.Name == name && r.Id != id) != null)
                    return true;
            }
            return false;
        }

        public async Task<bool> Update(Entities.Doctor Doctor)
        {
            _context.Update(Doctor);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
