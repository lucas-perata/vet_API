using API.Dtos.Photo;
using API.Entities;

namespace API.Dtos.Adoption
{
    public class AdoptionDto
    {
        public int Id { get; set; }
        public string AppUserId { get; set; }
        public bool IsNeutered { get; set; }
        public string Name { get; set; }
        public bool IsVaccinated { get; set; }
        public bool IsDeworm { get; set; }
        public string Status { get; set; }
        public string Gender { get; set; }
        public StatusList StatusList { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Area { get; set; }
        public string Province { get; set; }
        public string Description { get; set; }
        public ICollection<AdoptionPhotoDto> AdoptionPhoto { get; set; }
    }
}
