namespace API.Dtos.Appointment
{
    public class RequestAppointmentDto
    {
        public DateTime Date { get; set; }
        public string OwnerId { get; set; }
        public string VetId { get; set; }
        public int PetId { get; set; }
        public int ServiceId { get; set; }
        public string Motive { get; set; }
    }
}

