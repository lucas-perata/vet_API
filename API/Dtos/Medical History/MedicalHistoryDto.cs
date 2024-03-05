namespace API.Dtos
{
    public class MedicalHistoryDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string Treatment { get; set; }
        public string VetName { get; set; }
        public int PetId { get; set; }
    }
}

