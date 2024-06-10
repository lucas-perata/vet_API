using API.Entities.Identity;

namespace API.Entities
{
    public class Appointment
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string OwnerId { get; set; }
        public AppUser Owner { get; set; }
        public string VetId { get; set; }
        public AppUser Vet { get; set; }
        public int? PetId { get; set; }
        public Pet Pet { get; set; }
        public AppointmentStatus Status { get; set; }
        public int ServiceId { get; set; }
        public Service Service { get; set; }
    }

    public enum AppointmentStatus
    {
        Requested,
        Accepted,
        Rejected,
        DateChangeRequested
    }
}

