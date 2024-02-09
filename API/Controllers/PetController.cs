using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
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
        private readonly IPetRepository _petRepository; 
        private readonly IMapper _mapper;
        public PetController(IMapper mapper,IPetRepository petRepository, UserManager<AppUser> userManager)
        {
            _petRepository = petRepository;
            _userManager = userManager; 
            _mapper = mapper;
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<PetDto>> GetPet(int id)
        {
            return await _petRepository.GetPet(id);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<PetDto>> CreatePet(CreatePetDto createPetDto)
        {
            var email = User.FindFirstValue(ClaimTypes.Email); 
            var user = await _userManager.FindByEmailAsync(email); 

            var pet = new Pet {
                OwnerId = user.Id,
                Name = createPetDto.Name
            }; 

            _petRepository.AddPet(pet); 

            if (await _petRepository.Complete()) return Ok(_mapper.Map<PetDto>(pet));

            return BadRequest("Failed to create pet");
        }
    }
}