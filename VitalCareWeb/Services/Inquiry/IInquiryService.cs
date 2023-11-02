namespace VitalCareWeb.Services.Inquiry;
using VitalCareWeb.Entities;
public interface IInquiryService
{
    Task<Inquiry?> GetById(int id);
    Task<IEnumerable<Inquiry>> GetAll();
    Task<bool> Add(Inquiry inquiry);
}
