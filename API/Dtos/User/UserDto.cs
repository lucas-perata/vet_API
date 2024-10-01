using API.Entities;

namespace API.Dtos
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string DisplayName { get; set; }
        public List<Entities.Photo> Photos { get; set; }
    }
}

