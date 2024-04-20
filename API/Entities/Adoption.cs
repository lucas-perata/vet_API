using API.Entities.Identity;

namespace API.Entities
{
    public class Adoption
    {
        public int Id { get; set; }
        public string AppUserId { get; set; }
        public string Name { get; set; }
        public AppUser AppUser { get; set; }
        public bool IsNeutered { get; set; }
        public bool IsVaccinated { get; set; }
        public bool IsDeworm { get; set; }
        public string Status { get; set; }
        public string Gender { get; set; }
        public StatusList StatusList { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Area { get; set; }
        public string Province { get; set; }
        public ICollection<MedicalHistory> MedicalHistories { get; set; }
        public string Description { get; set; }
        public List<AdoptionPhoto> AdoptionPhotos { get; set; } = new();
    }

    public enum StatusList
    {
        Open,
        InProcess,
        Adopted
    }
}
