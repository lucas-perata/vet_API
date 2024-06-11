namespace API.Dtos
{
    public class FollowerDto
    {
        public string FollowerId { get; set; }
        public string FollowerUsername { get; set; }
        public string FollowedId { get; set; }
        public DateTime Date { get; set; }
    }
}
