using API.Entities;

namespace API.Dtos.Review
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public ReviewStar Stars { get; set; }
        public string Body { get; set; }
        public string OwnerId { get; set; }
        public string VetId { get; set; }
        public DateTime Date { get; set; }
    }
}

