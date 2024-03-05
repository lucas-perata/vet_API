namespace API.Dtos
{
    public class VaccineDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Required { get; set; }
        public string SideEffects { get; set; }
    }
}
