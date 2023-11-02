namespace VitalCareWeb.Services.CounterRecord;
using VitalCareWeb.Entities;

public interface ICounterRecordService
{
    Task<bool> IsDublicate(int id, string name);
    Task<CounterRecord?> GetById(int id);
    Task<IEnumerable<CounterRecord>> GetAll();
    Task<bool> Add(CounterRecord record);
    Task<bool> Update(CounterRecord record);
    Task<bool> Delete(int id);
}
