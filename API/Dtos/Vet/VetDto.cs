using API.Entities.Identity;

namespace API.Dtos
{
    public class VetDto
    {
        public string Id { get; set; }
        public AppUser user { get; set; }
    }
}
