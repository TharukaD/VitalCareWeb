namespace VitalCareWeb.Services.Appointment;
using VitalCareWeb.Entities;
public interface IAppointmentService
{
    Task<Appointment?> GetById(int id);
    Task<IEnumerable<Appointment>> GetAll();
    Task<bool> Add(Appointment appointment);
}
