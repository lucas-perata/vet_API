namespace API.Entities
{
    public class PetVaccine
    {
        public int Id { get; set; }
        public int PetId { get; set; }
        public int VaccineId { get; set; }
        public Pet Pet { get; set; }
        public Vaccine Vaccine { get; set; }
    }
}
