using API.Entities;

namespace API.Interfaces
{
    public interface IFollowerInterface
    {
        Task<bool> Complete();
        Task<int> GetFollowersForVet(string vetId);
        Task<bool> IsFollowing(string followerId, string followedId);
        void AddFollower(Follower follower);
        bool DeleteFollower(Follower follower);
    }
}
