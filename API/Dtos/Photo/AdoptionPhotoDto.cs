using System.ComponentModel.DataAnnotations.Schema;

namespace API.Dtos.Photo
{
    [Table("Adoption-Photos")]
    public class AdoptionPhotoDto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string PublicId { get; set; }
        public bool IsMain { get; set; }
        public int AdoptionId { get; set; }
    }
}
