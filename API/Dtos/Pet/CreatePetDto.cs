namespace API.Dtos
{
    public class CreatePetDto
    {
        public string Name { get; set; }
        public string Breed { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Color { get; set; }
        public string Gender { get; set; }
        public int Weight { get; set; }
        public bool IsNeutered { get; set; }
        public bool ForAdoption { get; set; }
        public IFormFile? Photo { get; set; }
    }
}
