using API.Entities;

namespace API.Dtos.Adoption
{
    public class CreateAdoptionDto
    {
        public bool IsNeutered { get; set; }
        public bool IsVaccinated { get; set; }
        public bool IsDeworm { get; set; }
        public string Area { get; set; }
        public string Province { get; set; }
        public StatusList StatusList { get; set; }
    }
}
