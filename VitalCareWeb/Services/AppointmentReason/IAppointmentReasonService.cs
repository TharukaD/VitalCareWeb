namespace VitalCareWeb.Services.AppointmentReason;
using VitalCareWeb.Entities;

public interface IAppointmentReasonService
{
    Task<bool> IsDublicate(int id, string name);
    Task<AppointmentReason?> GetById(int id);
    Task<IEnumerable<AppointmentReason>> GetAll();
    Task<bool> Add(AppointmentReason location);
    Task<bool> Update(AppointmentReason location);
    Task<bool> Delete(int id);
}
