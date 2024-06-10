using API.Entities.Identity;

namespace API.Entities
{
    public class Follower
    {
        public int Id { get; set; }
        public string FollowedId { get; set; }
        public AppUser Vet { get; set; }
        public string FollowerId { get; set; }
        public AppUser Owner { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
    }
}
