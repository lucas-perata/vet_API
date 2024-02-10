using System.Security.Claims;
using API.Dtos;
using API.Entities;
using API.Entities.Identity;
using API.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly PetRepository _petRepository; 
        private readonly IMapper _mapper;
        public PetController(IMapper mapper,PetRepository petRepository, UserManager<AppUser> userManager)
        {
            _petRepository = petRepository;
            _userManager = userManager; 
            _mapper = mapper;
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<PetDto>> GetPet(int id)
        {
            var pet =  await _petRepository.GetPet(id);

            return new PetDto 
            {
                Name = pet.Name
            };
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<PetDto>> CreatePet(CreatePetDto createPetDto)
        {
            var email = User.FindFirstValue(ClaimTypes.Email); 
            var user = await _userManager.FindByEmailAsync(email); 

            var pet = new Pet {
                OwnerId = user.Id,
                Name = createPetDto.Name,
                Breed = createPetDto.Breed,
                DateOfBirth = createPetDto.DateOfBirth.ToUniversalTime(),
                Color = createPetDto.Color,
                Gender = createPetDto.Gender,
                Weight = createPetDto.Weight,
                IsNeutered = createPetDto.IsNeutered
            }; 

            _petRepository.AddPet(pet); 

            if (await _petRepository.Complete()) return Ok(_mapper.Map<PetDto>(pet));

            return BadRequest("Failed to create pet");
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePet(int id, UpdatePetDto updatePetDto)
        {
            var pet = await _petRepository.GetPet(id); 
            
            if(pet == null) return NotFound();

            var email = User.FindFirstValue(ClaimTypes.Email); 
            var user = await _userManager.FindByEmailAsync(email); 

            if(pet.OwnerId != user.Id) return BadRequest("Not your pet"); 

            _mapper.Map(updatePetDto, pet); 
            
            _petRepository.UpdatePet(pet); 

            if (await _petRepository.Complete()) return Ok(); 

            return BadRequest("Failed to update pet");

        }

        [Authorize]
        [HttpDelete("{id}")]

        public async Task<ActionResult> DeletePet(int id)
        {
            var pet = await _petRepository.GetPet(id); 

            var email = User.FindFirstValue(ClaimTypes.Email); 
            var user = await _userManager.FindByEmailAsync(email); 

            if(pet.OwnerId != user.Id) return BadRequest("Not your pet"); 

            if(pet == null) return NotFound(); 

            var result = _petRepository.DeletePet(pet);

            if(!result) return BadRequest("There was a problem deleting the pet"); 

            return NoContent();
        }
    }
}