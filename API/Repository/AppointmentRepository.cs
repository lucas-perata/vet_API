using API.Data;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class AppointmentRepository : IAppointmentInterface
    {
        private readonly DataContext _context;
        public AppointmentRepository(DataContext context)
        {
            _context = context; 
        }

        public async Task<bool> Complete()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void CreateAppointment(Appointment appointment)
        {
            _context.Appointments.Add(appointment); 
        }

        public bool DeleteAppointment(Appointment appointment)
        {
            _context.Remove(appointment); 
            return _context.SaveChanges() > 0;
        }

        public async Task<Appointment> GetAppointment(int id)
        {
            return await _context.Appointments.FindAsync(id);
        }

        public async Task<List<Appointment>> GetAppointmentsForOwner(string ownerId)
        {
            return await _context.Appointments
                            .Where(a => a.OwnerId == ownerId)
                            .ToListAsync(); 
        }

        public async Task<List<Appointment>> GetAppointmentsForVet(string vetId)
        {
            return await _context.Appointments
                            .Where(a => a.VetId == vetId)
                            .ToListAsync();
        }

        public void UpdateAppointment(Appointment appointment)
        {
            _context.Entry(appointment).State = EntityState.Modified;
        }
    }
}