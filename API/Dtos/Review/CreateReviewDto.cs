using API.Entities;

namespace API.Dtos.Review
{
    public class CreateReviewDto
    {
        public ReviewStar Stars { get; set; }
        public string Body { get; set; }
        public string VetId { get; set; }
    }
}