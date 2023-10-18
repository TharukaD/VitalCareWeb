namespace VitalCareWeb.Services.Location;
using VitalCareWeb.Entities;
public interface ILocationService
{
    Task<bool> IsDublicate(int id, string name);
    Task<Location?> GetById(int id);
    Task<IEnumerable<Location>> GetAll();
    Task<bool> Add(Location location);
    Task<bool> Update(Location location);
    Task<(bool, string)> Delete(int id);
}
