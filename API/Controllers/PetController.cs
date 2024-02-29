using System.Security.Claims;
using API.Dtos;
using API.Dtos.Photo;
using API.Entities;
using API.Entities.Identity;
using API.Interfaces;
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
    public class PetController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly PetRepository _petRepository; 
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;
        public PetController(IMapper mapper,PetRepository petRepository, 
        UserManager<AppUser> userManager, IPhotoService photoService)
        {
            _petRepository = petRepository;
            _userManager = userManager; 
            _mapper = mapper;
            _photoService = photoService;
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<PetDto>> GetPet(int id)
        {
            var pet =  await _petRepository.GetPet(id);

            return Ok(_mapper.Map<PetDto>(pet));
        }

        [HttpGet("pets")]
        public async Task<ActionResult<List<PetDto>>> GetPetsForOwner()
        {
            var email = User.FindFirstValue(ClaimTypes.Email); 
            var user = await _userManager.FindByEmailAsync(email);

            if(user is null)  return Unauthorized();

            var pets = await _petRepository.GetPetsForOwner(user.Id); 

            if(pets is null) return NotFound();

            
            var petDtos = _mapper.Map<List<PetDto>>(pets);

            return Ok(petDtos); 
        }

        [HttpPost]
        public async Task<ActionResult<PetDto>> CreatePet(CreatePetDto createPetDto)
        {
            var email = User.FindFirstValue(ClaimTypes.Email); 
            var user = await _userManager.FindByEmailAsync(email); 

            var pet = new Pet {
                OwnerId = user.Id,
                Name = createPetDto.Name,
                Breed = createPetDto.Breed,
                DateOfBirth = createPetDto.DateOfBirth,
                Color = createPetDto.Color,
                Gender = createPetDto.Gender,
                Weight = createPetDto.Weight,
                IsNeutered = createPetDto.IsNeutered
            }; 

            _petRepository.AddPet(pet); 

            if (await _petRepository.Complete()) return Ok(_mapper.Map<PetDto>(pet));

            return BadRequest("Failed to create pet");
        }

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

        [HttpDelete("{id}")]

        public async Task<ActionResult> DeletePet(int id)
        {
            var pet = await _petRepository.GetPet(id); 
            
            if(pet == null) return NotFound(); 

            var email = User.FindFirstValue(ClaimTypes.Email); 
            var user = await _userManager.FindByEmailAsync(email); 

            if(pet.OwnerId != user.Id) return BadRequest("Not your pet"); 

            var result = _petRepository.DeletePet(pet);

            if(!result) return BadRequest("There was a problem deleting the pet"); 

            return NoContent();
        }

        [HttpPost("add-photo/{petId}")]
        public async Task<ActionResult<PetPhotoDto>> AddPhoto(IFormFile file, int petId)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var user = await _userManager.FindByEmailAsync(email);

            var pet = await _petRepository.GetPet(petId);

            if(user is null) return NotFound(); 

            if(pet is null) return NotFound();

            var result = await _photoService.AddPhotoAsync(file);

            if(result.Error != null) return BadRequest(result.Error.Message);

            var photo = new PetPhoto 
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId,
                PetId = petId
            };

            if(pet.PetPhotos.Count == 0) photo.IsMain = true;

            pet.PetPhotos.Add(photo);

            if( await _petRepository.Complete()) return _mapper.Map<PetPhotoDto>(photo);

            return BadRequest("Problem adding photo");
        }

        [HttpPut("set-main-photo-pet/{photoId}")] 
        public async Task<ActionResult> SetMainPhoto(int photoId, [FromBody]DeletePhotoRequest deletePhotoRequest)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);

            if(user is null) return NotFound();

            var pet = await _petRepository.GetPet(deletePhotoRequest.PetId);

            if(pet is null) return NotFound();

            var photo = pet.PetPhotos.FirstOrDefault(x => x.Id == photoId);

            if(photo is null) return NotFound();

            if(photo.IsMain)
            {
                return BadRequest("This is already the main photo");
            }

            var currentMain = pet.PetPhotos.FirstOrDefault(x => x.IsMain);
            if(currentMain != null) currentMain.IsMain = false;
            photo.IsMain = true;

            if(await _petRepository.Complete()) return NoContent();

            return BadRequest("Problem setting main photo for pet");
        }

        public class DeletePhotoRequest
        {
            public int PetId { get; set; }
        }

        // TODO not working properly

        [HttpDelete("delete-photo-pet/{photoId}")]
        public async Task<ActionResult> DeletePhoto([FromBody]DeletePhotoRequest deletePhotoRequest, [FromQuery] int photoId)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);

            if(user is null) return NotFound("User");

            var pet = await _petRepository.GetPet(deletePhotoRequest.PetId);

            if(pet is null) return NotFound("Pet");

            var photo = pet.PetPhotos.FirstOrDefault(x => x.Id == photoId);

            if(photo is null) return NotFound("Photo");

            if(photo.IsMain) return BadRequest("Cannot delete main photo");

            if(photo.PublicId != null) 
            {
                var result = await _photoService.DeletePhotoAsync(photo.PublicId);
                if(result.Error != null) return BadRequest(result.Error.Message);
            }
            
            pet.PetPhotos.Remove(photo);

            if(await _petRepository.Complete() ) return Ok(); 

            return BadRequest("Problem deleting photo");
        }
    }
}