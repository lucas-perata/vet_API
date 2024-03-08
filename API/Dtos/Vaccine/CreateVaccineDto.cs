namespace API.Dtos
{
    public class CreateVaccineDto
    {
        public string Name { get; set; }
        public bool Required { get; set; }
        public string SideEffects { get; set; }
    }
}
