using API.Dtos;
using API.Entities;
using API.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class MedicalHistoryController : ControllerBase
    {   
        private readonly IMapper _mapper; 
        private readonly MedicalHistoryRepository _mRepository; 
        private readonly PetRepository _petRepository;
        public MedicalHistoryController(IMapper mapper, MedicalHistoryRepository mRepository, PetRepository petRepository)
        {
            _mapper = mapper; 
            _mRepository = mRepository;
            _petRepository = petRepository;
        }  

        [HttpGet("{id}")]
        public async Task<ActionResult<MedicalHistoryDto>> GetMedicalHistory(int id)
        {
            var medicalHistory = await _mRepository.GetMedicalHistory(id); 
            return new MedicalHistoryDto
            {
                Date = medicalHistory.Date,
                Description = medicalHistory.Description,
                Treatment = medicalHistory.Treatment,
                VetName = medicalHistory.VetName,
                PetId = medicalHistory.PetId
            };
        }

        [HttpGet("pet/{petId}")]
        public async Task<ActionResult<List<MedicalHistory>>> GetAllMedicalHistoryForPet(int petId)
        {
            var pet = await _petRepository.GetPet(petId); 

            if(pet == null) return NotFound("Pet not found"); 

            return await _mRepository.GetAllMedicalHistoryForPet(petId); 
        }

        [HttpPost("{petId}")]
        public async Task<ActionResult<MedicalHistoryDto>> CreateMedicalHistory(CreateMedicalHistoryDto createMedicalHistoryDto, int petId)
        {
            var pet = await _petRepository.GetPet(petId); 
            
            if(pet == null) return NotFound("Pet not found"); 

            var medicalHistory = new MedicalHistory {
                Date = createMedicalHistoryDto.Date.ToUniversalTime(),
                Description = createMedicalHistoryDto.Description,
                Treatment = createMedicalHistoryDto.Treatment,
                VetName = createMedicalHistoryDto.VetName,
                PetId = pet.Id
            };

            _mRepository.AddMedicalHistory(medicalHistory); 

            if(await _mRepository.Complete()) return Ok(_mapper.Map<MedicalHistoryDto>(medicalHistory)); 

            return BadRequest("Failed to add medical history"); 
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateMedicalHistory(int id, UpdateMedicalHistoryDto updateMedicalHistoryDto)
        {
            var medicalHistory = await _mRepository.GetMedicalHistory(id);

            if(medicalHistory == null) return NotFound(); 

            _mapper.Map(updateMedicalHistoryDto, medicalHistory); 

            _mRepository.UpdateMedicalHistory(medicalHistory); 

            if(await _mRepository.Complete()) return Ok(); 

            return BadRequest("Failed to update medical history"); 
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMedicalHistory(int id)
        {
            var medicalHistory = await _mRepository.GetMedicalHistory(id); 

            if(medicalHistory == null) return NotFound(); 

            var result = _mRepository.DeleteMedicalHistory(medicalHistory);

            if(!result) return BadRequest("There was a problem deleting the mh"); 

            return NoContent(); 
        }
    }
}