using System.Security.Claims;
using API.Dtos;
using API.Entities.Identity;
using API.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly DashboardRepository _dashboardRepository;

        public DashboardController(
            UserManager<AppUser> userManager,
            DashboardRepository dashboardRepository
        )
        {
            _userManager = userManager;
            _dashboardRepository = dashboardRepository;
        }

        [HttpGet("/vet-dash")]
        public async Task<ActionResult<VetDashDto>> GetDataForVetDash()
        {
            string email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);

            return Ok();
        }

        [HttpGet("/owner-dash")]
        public async Task<ActionResult<OwnerDashDto>> GetDataForOwnerDash()
        {
            string email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);

            return Ok();
        }
    }
}
