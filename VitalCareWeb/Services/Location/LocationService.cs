using Microsoft.EntityFrameworkCore;
using VitalCareWeb.Data;

namespace VitalCareWeb.Services.Location
{
    public class LocationService : ILocationService
    {
        private ApplicationDbContext _context;
        public LocationService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Entities.Location?> GetById(int id)
        {
            return await _context.FindAsync<Entities.Location>(id);
        }

        public async Task<IEnumerable<Entities.Location>> GetAll()
        {
            return await _context.Locations.ToListAsync();
        }

        public async Task<bool> Add(Entities.Location Location)
        {
            _context.Locations.Add(Location);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<(bool, string)> Delete(int id)
        {
            var location = await _context.Locations.Include(r => r.Doctors).SingleOrDefaultAsync(r => r.Id == id);
            if (location != null)
            {
                if (location.Doctors.Any())
                {
                    return (false, "Location already assigned with doctor");
                }

                _context.Locations.Remove(location);
                await _context.SaveChangesAsync();
                return (true, "Success");
            }
            return (false, "Location not found");
        }

        public async Task<bool> IsDublicate(int id, string name)
        {
            if (id == 0)
            {
                if (await _context.Locations.FirstOrDefaultAsync(r => r.Name == name) != null)
                    return true;
            }
            else
            {
                if (await _context.Locations.FirstOrDefaultAsync(r => r.Name == name && r.Id != id) != null)
                    return true;
            }
            return false;
        }

        public async Task<bool> Update(Entities.Location Location)
        {
            _context.Update(Location);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
