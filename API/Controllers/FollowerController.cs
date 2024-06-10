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

            if (await _userManager.FindByIdAsync(followDto.FollowedId) is null)
                return BadRequest();

            if (user.Id == followDto.FollowedId)
                return BadRequest("Cannot follow yourself");

            if (_followerRepository.IsFollowing(user.Id, followDto.FollowedId) is not null)
                return BadRequest("You are already following them");

            var follow = new Follower { FollowerId = user.Id, FollowedId = followDto.FollowedId, };

            _followerRepository.AddFollower(follow);

            if (await _followerRepository.Complete() is false)
                return BadRequest();

            return Ok(_mapper.Map<FollowerDto>(follow));
        }

        // [HttpDelete("{id}")]
        // public async Task<ActionResult> DeleteFollower(int id)
        // {
        //   var follower;
        // }
    }
}
