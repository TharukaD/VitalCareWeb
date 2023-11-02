namespace VitalCareWeb.Services.Inquiry;

using Microsoft.EntityFrameworkCore;
using VitalCareWeb.Data;
using VitalCareWeb.Entities;

public class InquiryService : IInquiryService
{
    private ApplicationDbContext _context;
    public InquiryService(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Inquiry?> GetById(int id)
    {
        return await _context.FindAsync<Inquiry>(id);
    }

    public async Task<IEnumerable<Inquiry>> GetAll()
    {
        return await _context.Inquiries.OrderByDescending(r => r.CreatedOn).ToListAsync();
    }

    public async Task<bool> Add(Inquiry inquiry)
    {
        _context.Inquiries.Add(inquiry);
        await _context.SaveChangesAsync();
        return true;
    }
}