namespace VitalCareWeb.Services.WhyChooseUsRecord;
using VitalCareWeb.Entities;

public interface IWhyChooseUsRecordService
{
    Task<bool> IsDublicate(int id, string name);
    Task<WhyChooseUsRecord?> GetById(int id);
    Task<IEnumerable<WhyChooseUsRecord>> GetAll();
    Task<bool> Add(WhyChooseUsRecord record);
    Task<bool> Update(WhyChooseUsRecord record);
    Task<bool> Delete(int id);
}
