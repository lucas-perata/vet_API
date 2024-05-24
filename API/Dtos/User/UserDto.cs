namespace API.Dtos
{
    public class UserDto
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string DisplayName { get; set; }
        public List<Entities.Photo> Photos { get; set; }
    }
}
