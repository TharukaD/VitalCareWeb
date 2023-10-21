namespace VitalCareWeb.Services.Doctor;
using VitalCareWeb.Entities;
public interface IDoctorService
{
    Task<bool> IsDublicate(int id, string name);
    Task<Doctor?> GetById(int id);
    Task<IEnumerable<Doctor>> GetAll();
    Task<IEnumerable<Doctor>> GetAllFiltered(string? doctorName, List<int>? locations, List<string>? genders, List<int>? specialities);
    Task<bool> Add(Doctor doctor);
    Task<bool> Update(Doctor doctor);
    Task<(bool, string)> Delete(int id);
}
