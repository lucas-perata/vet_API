using API.Entities;

namespace API.Dtos.Appointment
{
    public class AppointmentDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string OwnerId { get; set; }
        public string VetId { get; set; }
        public int PetId { get; set; }
        public string Motive { get; set; }
        public AppointmentStatus Status { get; set; }
    }
}

