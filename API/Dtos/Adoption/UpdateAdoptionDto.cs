using API.Entities;

namespace API.Dtos.Adoption
{
    public class UpdateAdoptionDto
    {
        public bool IsNeutered { get; set; }
        public bool IsVaccinated { get; set; }
        public bool IsDeworm { get; set; }
        public string Area { get; set; }
        public string Province { get; set; }
        public StatusList StatusList { get; set; }
        public string Description { get; set; }
    }
}
