using API.Entities.Identity;

namespace API.Dtos
{
    public class VetDto
    {
        public string Id { get; set; }
        public string Province { get; set; }
        public string Area { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
    }
}
