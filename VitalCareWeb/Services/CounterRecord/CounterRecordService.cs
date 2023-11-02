using VitalCareWeb.Data;

namespace VitalCareWeb.Services.CounterRecord;

using Microsoft.EntityFrameworkCore;
using VitalCareWeb.Entities;
public class CounterRecordService : ICounterRecordService
{
    private ApplicationDbContext _context;
    public CounterRecordService(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<CounterRecord?> GetById(int id)
    {
        return await _context.FindAsync<CounterRecord>(id);
    }

    public async Task<IEnumerable<CounterRecord>> GetAll()
    {
        return await _context.CounterRecords.OrderBy(r => r.Priority).ToListAsync();
    }

    public async Task<bool> Add(CounterRecord record)
    {
        _context.CounterRecords.Add(record);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Delete(int id)
    {
        var record = await _context.CounterRecords.SingleOrDefaultAsync(r => r.Id == id);
        if (record != null)
        {
            _context.CounterRecords.Remove(record);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<bool> IsDublicate(int id, string name)
    {
        if (id == 0)
        {
            if (await _context.CounterRecords.FirstOrDefaultAsync(r => r.Name == name) != null)
                return true;
        }
        else
        {
            if (await _context.CounterRecords.FirstOrDefaultAsync(r => r.Name == name && r.Id != id) != null)
                return true;
        }
        return false;
    }

    public async Task<bool> Update(CounterRecord record)
    {
        _context.Update(record);
        await _context.SaveChangesAsync();
        return true;
    }
}
