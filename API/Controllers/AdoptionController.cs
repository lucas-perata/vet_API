using System.Security.Claims;
using API.Dtos;
using API.Dtos.Adoption;
using API.Entities;
using API.Entities.Identity;
using API.Extensions;
using API.Helpers;
using API.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AdoptionController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly AdoptionRepository _adoptionRepository;
        private readonly UserManager<AppUser> _userManager;

        public AdoptionController(
            IMapper mapper,
            AdoptionRepository adoptionRepository,
            UserManager<AppUser> userManager
        )
        {
            _adoptionRepository = adoptionRepository;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AdoptionDto>> GetAdoption(int id)
        {
            var adoption = await _adoptionRepository.GetAdoption(id);
            if (adoption is null)
                return NotFound();
            return Ok(_mapper.Map<AdoptionDto>(adoption));
        }

        [HttpGet("all-adoptions")]
        public async Task<ActionResult<PagedList<AdoptionDto>>> GetAdoptions(
            [FromQuery] UserParams userParams
        )
        {
            var adoptions = await _adoptionRepository.GetAdoptions(userParams);

            Response.AddPaginationHeader(
                new PaginationHeader(
                    adoptions.CurrentPage,
                    adoptions.PageSize,
                    adoptions.TotalCount,
                    adoptions.TotalPages
                )
            );

            return Ok(adoptions);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAdoption(CreateAdoptionDto createAdoptionDto)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);

            // TODO: check user type




            var adoption = new Adoption
            {
                AppUserId = user.Id,
                IsNeutered = createAdoptionDto.IsNeutered,
                IsDeworm = createAdoptionDto.IsDeworm,
                IsVaccinated = createAdoptionDto.IsVaccinated,
                Area = createAdoptionDto.Area,
                Province = createAdoptionDto.Province,
                Description = createAdoptionDto.Description,
                Name = createAdoptionDto.Name,
                StatusList = 0,
            };

            _adoptionRepository.CreateAdoptionAsync(adoption);

            var adoptionDto = _mapper.Map<CreateAdoptionDto>(adoption);

            if (await _adoptionRepository.Complete())
            {
                return Ok(adoptionDto);
            }

            return BadRequest("Failed to create adoption");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAdoption(int id, UpdateAdoptionDto updateAdoptionDto)
        {
            var adoption = await _adoptionRepository.GetAdoption(id);

            if (adoption is null)
                return NotFound();

            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);

            if (adoption.AppUserId != user.Id)
                return BadRequest("You cannot edit this adoption");

            _mapper.Map(updateAdoptionDto, adoption);

            _adoptionRepository.UpdateAdoption(adoption);

            if (await _adoptionRepository.Complete())
                return Ok();

            return BadRequest("Failed to update adoption");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAdoption(int id)
        {
            var adoption = await _adoptionRepository.GetAdoption(id);

            if (adoption is null)
                return NotFound();

            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);

            if (adoption.AppUserId != user.Id)
                return BadRequest("Not your adoption");

            var result = _adoptionRepository.DeleteAdoption(adoption);

            if (!result)
                return BadRequest("There was a problem deleting the adoption");

            return NoContent();
        }

        [HttpGet("search")]
        public async Task<ActionResult<PagedList<AdoptionDto>>> SearchAdoptions(
            [FromQuery] UserParams userParams,
            string area,
            string province,
            string gender
        )
        {
            var adoptions = await _adoptionRepository.SearchAdoptions(
                userParams,
                gender,
                area,
                province
            );

            if (adoptions is null)
                return NotFound();

            Response.AddPaginationHeader(
                new PaginationHeader(
                    adoptions.CurrentPage,
                    adoptions.PageSize,
                    adoptions.TotalCount,
                    adoptions.TotalPages
                )
            );

            return Ok(adoptions);
        }
    }
}
