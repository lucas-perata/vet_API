using API.Dtos.Photo;

namespace API.Dtos
{
    public class PetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Breed { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Color { get; set; }
        public string Gender { get; set; }
        public int Weight { get; set; }
        public bool IsNeutered { get; set; }
        public bool ForAdoption { get; set; }
        public string OwnerId { get; set; }
        public List<PetPhotoDto> PetPhoto { get; set; }
    }
}

