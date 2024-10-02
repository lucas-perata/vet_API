using System.Security.Claims;
using API.Dtos;
using API.Entities.Identity;
using API.Extensions;
using API.Helpers;
using API.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class VetController : ControllerBase
    {
        private readonly VetsRepository _vetRepository;
        private readonly UserManager<AppUser> _userManager;

        public VetController(UserManager<AppUser> userManager, VetsRepository vetsRepository)
        {
            _vetRepository = vetsRepository;
            _userManager = userManager;
        }

        [Authorize]
        [HttpGet("all-vets")]
        public async Task<ActionResult<PagedList<VetDto>>> GetVets([FromQuery] UserParams userParams)
        {
            string email = User.FindFirstValue(ClaimTypes.Email);
            AppUser user = await _userManager.FindByEmailAsync(email);

            if (user is null)
                return Unauthorized();

            PagedList<VetDto> vets = await _vetRepository.GetVetList(userParams, user);

            Response.AddPaginationHeader(
                new PaginationHeader(
                  vets.CurrentPage,
                  vets.PageSize,
                  vets.TotalCount,
                  vets.TotalPages
                  )
                );

            if (vets is not null)
                return Ok(vets);

            return NotFound();

        }
    }
}
