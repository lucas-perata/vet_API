
using API.Dtos.Appointment;
using API.Entities;

namespace API.Interfaces
{
    public interface IAppointmentInterface
    {
       public Task<List<Appointment>> GetAppointmentsForVet(string vetId); 
       public Task<List<Appointment>> GetAppointmentsForOwner(string ownerId);
       public Task<Appointment> GetAppointment(int id); 
       void CreateAppointment(Appointment appointment);
       Task<bool> Complete(); 
       bool DeleteAppointment(Appointment appointment); 
       void UpdateAppointment(Appointment appointment);
    }
}