using Microsoft.EntityFrameworkCore;
using VitalCareWeb.Data;

namespace VitalCareWeb.Services.Serivice
{
    public class ServiceService : IServiceService
    {
        private ApplicationDbContext _context;
        public ServiceService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Entities.Service?> GetById(int id)
        {
            return await _context.Services.Include(r => r.Location).SingleOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<Entities.Service>> GetAll()
        {
            return await _context.Services
                .Include(r => r.Location)
                .OrderBy(r => r.Priority).ToListAsync();
        }

        public async Task<bool> Add(Entities.Service service)
        {
            _context.Services.Add(service);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var service = await _context.Services.FindAsync(id);
            if (service == null)
            {
                return false;
            }
            _context.Services.Remove(service);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> IsDublicate(int id, string name)
        {
            if (id == 0)
            {
                if (await _context.Services.FirstOrDefaultAsync(r => r.Name == name) != null)
                    return true;
            }
            else
            {
                if (await _context.Services.FirstOrDefaultAsync(r => r.Name == name && r.Id != id) != null)
                    return true;
            }
            return false;
        }

        public async Task<bool> Update(Entities.Service service)
        {
            _context.Update(service);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
