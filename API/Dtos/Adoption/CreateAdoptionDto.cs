using API.Entities;

namespace API.Dtos.Adoption
{
    public class CreateAdoptionDto
    {
        public string Name { get; set; }
        public bool IsNeutered { get; set; }
        public bool IsVaccinated { get; set; }
        public bool IsDeworm { get; set; }
        public string Area { get; set; }
        public string Province { get; set; }
        public StatusList StatusList { get; set; }
        public string Description { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public IFormFile Photo { get; set; }
    }
}
