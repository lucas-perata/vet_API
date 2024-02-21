using API.Entities;

namespace API.Dtos.Review
{
    public class UpdateReviewDto
    {
        public ReviewStar Stars { get; set; }
        public string Body { get; set; }
    }
}