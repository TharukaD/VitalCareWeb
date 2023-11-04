namespace VitalCareWeb.Services.Brand;
using VitalCareWeb.Entities;
public interface IBrandService
{
    Task<bool> IsDublicate(int id, string name);
    Task<Brand?> GetById(int id);
    Task<IEnumerable<Brand>> GetAll();
    Task<bool> Add(Brand brand);
    Task<bool> Update(Brand brand);
    Task<(bool, string)> Delete(int id);
}
