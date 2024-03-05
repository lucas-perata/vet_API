namespace API.Entities
{
    public class Vaccine
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Required { get; set; }
        public string SideEffects { get; set; }
        public ICollection<PetVaccine> PetVaccines { get; set; }
        public ICollection<Pet> Pets { get; set; }
    }
}
