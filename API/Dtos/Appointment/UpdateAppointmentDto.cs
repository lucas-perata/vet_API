using API.Entities;

namespace API.Dtos.Appointment
{
    public class UpdateAppointmentDto
    {
        public AppointmentStatus Status { get; set; }
        public DateTime Date { get; set; }
    }
}