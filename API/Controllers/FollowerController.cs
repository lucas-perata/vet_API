using System.Security.Claims;
using API.Dtos;
using API.Entities;
using API.Entities.Identity;
using API.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FollowerController : ControllerBase
    {
        private readonly FollowerRepository _followerRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public FollowerController(
            FollowerRepository followerRepository,
            UserManager<AppUser> userManager,
            IMapper mapper
        )
        {
            _followerRepository = followerRepository;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet("/total-followers")]
        public async Task<ActionResult<int>> GetTotalFollowers()
        {
            string email = User.FindFirstValue(ClaimTypes.Email);
            AppUser user = await _userManager.FindByEmailAsync(email);

            var total = _followerRepository.GetFollowersForVet(user.Id);

            return Ok(total);
        }

        [HttpPost]
        public async Task<ActionResult> FollowUser(FollowDto followDto)
        {
            string email = User.FindFirstValue(ClaimTypes.Email);
            AppUser user = await _userManager.FindByEmailAsync(email);
            string userName = user.DisplayName;

            if (await _userManager.FindByIdAsync(followDto.FollowedId) is null)
                return BadRequest();

            if (user.Id == followDto.FollowedId)
                return BadRequest("Cannot follow yourself");

            if (await _followerRepository.IsFollowing(user.Id, followDto.FollowedId) == true)
                return BadRequest("You are already following them");

            var follow = new Follower
            {
                FollowerId = user.Id,
                FollowerUsername = userName,
                FollowedId = followDto.FollowedId,
            };

            _followerRepository.AddFollower(follow);

            if (await _followerRepository.Complete() is false)
                return BadRequest();

            return Ok(_mapper.Map<FollowerDto>(follow));
        }

        [HttpDelete("{vetId}")]
        public async Task<ActionResult> UnfollowUser(string vetId)
        {
            string email = User.FindFirstValue(ClaimTypes.Email);
            AppUser user = await _userManager.FindByEmailAsync(email);

            if (await _userManager.FindByIdAsync(vetId) is null)
                return BadRequest();

            if (await _followerRepository.IsFollowing(user.Id, vetId) == false)
                return BadRequest("Not following them");

            var follow = await _followerRepository.GetFollowRelationship(vetId, user.Id);

            if (follow is null)
                return BadRequest();

            var delete = _followerRepository.DeleteFollower(follow);

            if (!delete)
                return BadRequest();

            return NoContent();
        }
    }
}
