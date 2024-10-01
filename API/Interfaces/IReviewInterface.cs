using API.Dtos.Review;
using API.Entities;
using API.Helpers;

namespace API.Interfaces
{
    public interface IReviewInterface
    {
        Task<bool> Complete();
        Task<PagedList<ReviewDto>> GetReviewsForVet(string vetId, UserParams userParams);
        Task<Review> GetReview(int id);
        void AddReview(Review review);
        void UpdateReview(Review review);
        bool DeleteReview(Review review);
    }
}

