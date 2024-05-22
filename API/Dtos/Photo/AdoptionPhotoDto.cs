namespace API.Dtos.Photo
{
    public class AdoptionPhotoDto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public bool IsMain { get; set; }
        public int AdoptionId { get; set; }
    }
}
