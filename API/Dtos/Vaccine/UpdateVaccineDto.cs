namespace API.Dtos
{
    public class UpdateVaccineDto
    {
        public string Name { get; set; }
        public bool Required { get; set; }
        public string SideEffects { get; set; }
    }
}
