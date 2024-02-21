using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class ReviewRepository : IReviewInterface
    {
        private readonly DataContext _context;
        public ReviewRepository(DataContext context)
        {
            _context = context;
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

        public async Task<List<Review>> GetReviewsForVet(string vetId)
        {
            return await _context.Reviews
                            .Where(r => r.VetId == vetId)
                            .ToListAsync();
        }

        public void UpdateReview(Review review)
        {
            _context.Entry(review).State = EntityState.Modified;
        }
    }
}