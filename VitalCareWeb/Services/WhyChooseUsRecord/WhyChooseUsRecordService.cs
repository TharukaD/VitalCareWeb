using VitalCareWeb.Data;

namespace VitalCareWeb.Services.WhyChooseUsRecord;

using Microsoft.EntityFrameworkCore;
using VitalCareWeb.Entities;
public class WhyChooseUsRecordService : IWhyChooseUsRecordService
{
    private ApplicationDbContext _context;
    public WhyChooseUsRecordService(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<WhyChooseUsRecord?> GetById(int id)
    {
        return await _context.FindAsync<WhyChooseUsRecord>(id);
    }

    public async Task<IEnumerable<WhyChooseUsRecord>> GetAll()
    {
        return await _context.WhyChooseUsRecords.OrderBy(r => r.Priority).ToListAsync();
    }

    public async Task<bool> Add(WhyChooseUsRecord record)
    {
        _context.WhyChooseUsRecords.Add(record);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Delete(int id)
    {
        var record = await _context.WhyChooseUsRecords.SingleOrDefaultAsync(r => r.Id == id);
        if (record != null)
        {
            _context.WhyChooseUsRecords.Remove(record);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<bool> IsDublicate(int id, string name)
    {
        if (id == 0)
        {
            if (await _context.WhyChooseUsRecords.FirstOrDefaultAsync(r => r.Name == name) != null)
                return true;
        }
        else
        {
            if (await _context.WhyChooseUsRecords.FirstOrDefaultAsync(r => r.Name == name && r.Id != id) != null)
                return true;
        }
        return false;
    }

    public async Task<bool> Update(WhyChooseUsRecord record)
    {
        _context.Update(record);
        await _context.SaveChangesAsync();
        return true;
    }
}
