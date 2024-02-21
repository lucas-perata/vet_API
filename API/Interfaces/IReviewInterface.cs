using API.Entities;

namespace API.Interfaces
{
    public interface IReviewInterface
    {
        Task<bool> Complete(); 
        Task<List<Review>> GetReviewsForVet(string vetId);
        Task<Review> GetReview(int id);
        void AddReview(Review review);
        void UpdateReview(Review review);
        bool DeleteReview(Review review);
    }
}