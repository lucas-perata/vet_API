namespace API.Dtos
{
    public class CreateMedicalHistoryDto
    {
        // TODO: VET NAME should be a list of vet existing
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string Treatment { get; set; }
        public string VetName { get; set; }
        public int PetId { get; set; }
    }
}
