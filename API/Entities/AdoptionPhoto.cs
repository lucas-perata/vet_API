using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Adoption-Photos")]
    public class AdoptionPhoto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string PublicId { get; set; }
        public bool IsMain { get; set; }
        public Adoption Adoption { get; set; }
        public int AdoptionId { get; set; }
    }
}
