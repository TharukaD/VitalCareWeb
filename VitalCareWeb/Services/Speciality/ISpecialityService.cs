namespace VitalCareWeb.Services.Speciality;
using VitalCareWeb.Entities;

public interface ISpecialityService
{
    Task<bool> IsDublicate(int id, string name);
    Task<Speciality?> GetById(int id);
    Task<IEnumerable<Speciality>> GetAll();
    Task<bool> Add(Speciality speciality);
    Task<bool> Update(Speciality speciality);
    Task<(bool, string)> Delete(int id);
}
