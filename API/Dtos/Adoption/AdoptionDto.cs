using API.Entities;

namespace API.Dtos.Adoption
{
    public class AdoptionDto
    {
        public int Id { get; set; }
        public int PetId { get; set; }
        public PetDto Pet { get; set; }
        public string AppUserId { get; set; }
        public bool IsNeutered { get; set; }
        public bool IsVaccinated { get; set; }
        public bool IsDeworm { get; set; }
        public string Area { get; set; }
        public string Province { get; set; }
        public StatusList StatusList { get; set; }
    }
}
