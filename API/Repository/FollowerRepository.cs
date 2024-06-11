using API.Data;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class FollowerRepository : IFollowerInterface
    {
        private readonly DataContext _context;

        public FollowerRepository(DataContext context)
        {
            _context = context;
        }

        public void AddFollower(Follower follower)
        {
            _context.Followers.Add(follower);
        }

        public async Task<bool> Complete()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public bool DeleteFollower(Follower follower)
        {
            _context.Remove(follower);
            return _context.SaveChanges() > 0;
        }

        public async Task<int> GetFollowersForVet(string vetId)
        {
            return await _context.Followers.CountAsync(f => f.FollowedId == vetId);
        }

        public async Task<Follower> GetFollowRelationship(string followedId, string followerId)
        {
            var followerRelationship = await _context
                .Followers.Where(f => f.FollowedId == followedId && f.FollowerId == followerId)
                .FirstOrDefaultAsync();

            return followerRelationship;
        }

        public async Task<bool> IsFollowing(string followerId, string followedId)
        {
            return await _context.Followers.AnyAsync(f =>
                f.FollowerId == followerId && f.FollowedId == followedId
            );
        }
    }
}
