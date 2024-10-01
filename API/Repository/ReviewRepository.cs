using API.Data;
using API.Dtos.Review;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class ReviewRepository : IReviewInterface
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ReviewRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void AddReview(Review review)
        {
            _context.Reviews.Add(review);
        }

        public async Task<bool> Complete()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public bool DeleteReview(Review review)
        {
            _context.Remove(review);
            return _context.SaveChanges() > 0;
        }

        public async Task<Review> GetReview(int id)
        {
            return await _context.Reviews.FindAsync(id);
        }

        public async Task<PagedList<ReviewDto>> GetReviewsForVet(
            string vetId,
            UserParams userParams
        )
        {
            var query = _context
                .Reviews.Where(r => r.VetId == vetId)
                .ProjectTo<ReviewDto>(_mapper.ConfigurationProvider)
                .AsNoTracking();

            return await PagedList<ReviewDto>.CreateAsync(
                query,
                userParams.PageNumber,
                userParams.PageSize
            );
        }

        public void UpdateReview(Review review)
        {
            _context.Entry(review).State = EntityState.Modified;
        }
    }
}

