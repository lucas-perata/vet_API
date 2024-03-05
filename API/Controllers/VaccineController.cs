using System.Collections;
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
    public class VaccineController : ControllerBase
    {
        private readonly VaccineRepository _vaccineRepository;
        private readonly PetRepository _petRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public VaccineController(
            VaccineRepository vaccineRepository,
            PetRepository petRepository,
            IMapper mapper,
            UserManager<AppUser> userManager
        )
        {
            _mapper = mapper;
            _userManager = userManager;
            _petRepository = petRepository;
            _vaccineRepository = vaccineRepository;
        }

        [HttpGet("pet/{petId}")]
        public async Task<ActionResult<List<VaccineDto>>> GetVaccinesForPet(int petId)
        {
            var pet = await _petRepository.GetPet(petId);

            if (pet is null)
                return NotFound("Pet not found");

            var vaccines = await _vaccineRepository.GetVaccinesForPet(petId);

            if (vaccines is null)
                return NotFound("Vaccines not found");

            return Ok(_mapper.Map<List<VaccineDto>>(vaccines));
        }

        [HttpGet("get-vaccines")]
        public async Task<ActionResult<IList>> GetVaccines()
        {
            var vaccines = await _vaccineRepository.GetVaccines();
            if (vaccines is null)
                return NotFound();
            return vaccines;
        }

        [HttpGet("{vaccineId}")]
        public async Task<ActionResult<Vaccine>> GetVaccine(int vaccineId)
        {
            var vaccine = await _vaccineRepository.GetVaccine(vaccineId);
            if (vaccine is null)
                return NotFound();
            return Ok(_mapper.Map<VaccineDto>(vaccine));
        }

        [HttpPost]
        public async Task<IActionResult> AddVaccine(CreateVaccineDto createVaccineDto)
        {
            var vaccine = new Vaccine
            {
                Name = createVaccineDto.Name,
                Required = createVaccineDto.Required,
                SideEffects = createVaccineDto.SideEffects,
            };

            _vaccineRepository.AddVaccine(vaccine);

            if (await _vaccineRepository.Complete())
                return Ok(_mapper.Map<VaccineDto>(vaccine));

            return BadRequest("Failed to create Vaccine");
        }

        [HttpPost("pet-vaccine/")]
        public async Task<IActionResult> AddVaccineToPet(
            int petId,
            AddPetVaccineDto addPetVaccineDto
        )
        {
            if (
                await _vaccineRepository.PetVaccineExistsAsync(
                    addPetVaccineDto.PetId,
                    addPetVaccineDto.VaccineId
                )
            )
            {
                return BadRequest("This pet already has the vaccine");
            }

            var vaccine = new PetVaccine
            {
                PetId = addPetVaccineDto.PetId,
                VaccineId = addPetVaccineDto.VaccineId,
            };

            _vaccineRepository.AddPetVaccine(vaccine);

            if (await _vaccineRepository.Complete())
            {
                var petVaccine = new AddPetVaccineDto
                {
                    PetId = vaccine.PetId,
                    VaccineId = vaccine.VaccineId
                };

                return Ok(petVaccine);
            }

            return BadRequest("There was a problem adding the relationship");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateVaccine(int id, UpdateVaccineDto updateVaccineDto)
        {
            var vaccine = await _vaccineRepository.GetVaccine(id);
            if (vaccine is null)
                return NotFound();

            _vaccineRepository.UpdateVaccine(vaccine);

            if (await _vaccineRepository.Complete())
                return Ok();

            return BadRequest("Failed to update vaccine");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteVaccine(int id)
        {
            var vaccine = await _vaccineRepository.GetVaccine(id);

            if (vaccine is null)
                return NotFound();

            var result = _vaccineRepository.DeleteVaccine(vaccine);

            if (!result)
                return BadRequest("There was a problem deleting the vaccine");

            return NoContent();
        }
    }
}
