using API.Entities.Identity;

namespace API.Entities
{
    public class Pet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Breed { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Color { get; set; }

        // TODO: GENDER list enum maybe
        public string Gender { get; set; }
        public int Weight { get; set; }
        public bool IsNeutered { get; set; }
        public bool? ForAdoption { get; set; }
        public string OwnerId { get; set; }
        public AppUser Owner { get; set; }
        public List<PetPhoto> PetPhotos { get; set; } = new();
        public ICollection<Spending> Spendings { get; set; }
        public ICollection<Vaccine> Vaccines { get; set; }
        public ICollection<PetVaccine> PetVaccines { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }
}
