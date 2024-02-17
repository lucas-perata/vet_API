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
        private readonly PetRepository _petRepository; 
        private readonly UserManager<AppUser> _userManager;
        public AdoptionController(IMapper mapper, AdoptionRepository adoptionRepository, 
        UserManager<AppUser> userManager, PetRepository petRepository)
        {
            _adoptionRepository = adoptionRepository; 
            _petRepository = petRepository;
            _mapper = mapper; 
            _userManager = userManager;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AdoptionDto>> GetAdoption(int id)
        {
            var adoption = await _adoptionRepository.GetAdoption(id); 
            return Ok(_mapper.Map<AdoptionDto>(adoption));
        }

        [HttpGet("/all-adoptions")]
        public async Task<ActionResult<PagedList<AdoptionDto>>> GetAdoptions([FromQuery] UserParams userParams)
        {
            var adoptions = await _adoptionRepository.GetAdoptions(userParams); 

            Response.AddPaginationHeader(new PaginationHeader(adoptions.CurrentPage, adoptions.PageSize, adoptions.TotalCount, adoptions.TotalPages));
            
            return Ok(adoptions);
        }


        [HttpPost]
        public async Task<IActionResult> CreateAdoptionWithPet(CreateAdoptionWithPetDto createAdoptionWithPetDto)
        {
            var email = User.FindFirstValue(ClaimTypes.Email); 
            var user = await _userManager.FindByEmailAsync(email); 

            // TODO: check user type 

            var pet = new Pet {
                OwnerId = user.Id,
                Name = createAdoptionWithPetDto.CreatePetDto.Name,
                Breed = createAdoptionWithPetDto.CreatePetDto.Breed, 
                DateOfBirth = createAdoptionWithPetDto.CreatePetDto.DateOfBirth,
                Color = createAdoptionWithPetDto.CreatePetDto.Color,
                Gender = createAdoptionWithPetDto.CreatePetDto.Gender,
                Weight = createAdoptionWithPetDto.CreatePetDto.Weight
            };

            _petRepository.AddPet(pet);

            if (await _petRepository.Complete() is false) return BadRequest("Failed to create pet entity"); 

            var petAdoption = await _petRepository.GetPet(pet.Id); 

            var adoption = new Adoption {
                AppUserId = pet.OwnerId,
                PetId = petAdoption.Id, 
                IsNeutered = createAdoptionWithPetDto.CreateAdoptionDto.IsNeutered, 
                IsDeworm = createAdoptionWithPetDto.CreateAdoptionDto.IsDeworm, 
                IsVaccinated = createAdoptionWithPetDto.CreateAdoptionDto.IsVaccinated,
            };

           _adoptionRepository.CreateAdoptionWithPetAsync(adoption); 

            var adoptionDto = _mapper.Map<CreateAdoptionDto>(adoption);
            var petDto = _mapper.Map<PetDto>(petAdoption);

           if (await _adoptionRepository.Complete())
           {
            var adoptionWithPet = new AdoptionWithPetDto 
            {
                Adoption = adoptionDto, 
                Pet = petDto,
            }; 

            return Ok(adoptionWithPet);
           }

            return BadRequest("Failed to create adoption"); 
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAdoption(int id, UpdateAdoptionDto updateAdoptionDto)
        {
            var adoption = await _adoptionRepository.GetAdoption(id); 

            if(adoption == null) return NotFound();
            
            var email = User.FindFirstValue(ClaimTypes.Email); 
            var user = await _userManager.FindByEmailAsync(email); 

            if(adoption.AppUserId != user.Id) return BadRequest("You cannot edit this adoption"); 

            _mapper.Map(updateAdoptionDto, adoption); 

            _adoptionRepository.UpdateAdoption(adoption); 

            if(await _adoptionRepository.Complete()) return Ok(); 

            return BadRequest("Failed to update adoption"); 
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAdoption(int id)
        {
            var adoption = await _adoptionRepository.GetAdoption(id); 

            if(adoption is null) return NotFound();

            var email = User.FindFirstValue(ClaimTypes.Email); 
            var user = await _userManager.FindByEmailAsync(email); 

            if(adoption.AppUserId != user.Id) return BadRequest("Not your adoption"); 

            var result = _adoptionRepository.DeleteAdoption(adoption); 
            
            if(!result) return BadRequest("There was a problem deleting the adoption"); 

            return NoContent();
        }
    }
}