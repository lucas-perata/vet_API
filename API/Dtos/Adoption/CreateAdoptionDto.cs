namespace API.Dtos.Adoption
{
    public class CreateAdoptionDto
    {
        public bool IsNeutered { get; set; }
        public bool IsVaccinated { get; set; }
        public bool IsDeworm { get; set; }
    }
}

