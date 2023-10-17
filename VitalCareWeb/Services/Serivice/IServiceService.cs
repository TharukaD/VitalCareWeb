namespace VitalCareWeb.Services.Serivice;
using VitalCareWeb.Entities;

public interface IServiceService
{
    Task<Service?> GetById(int id);
    Task<IEnumerable<Service>> GetAll();
    Task<bool> Add(Service speciality);
    Task<bool> Update(Service speciality);
    Task<bool> Delete(int id);
    Task<bool> IsDublicate(int id, string name);
}
