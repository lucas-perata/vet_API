using System.Security.Claims;
using API.Dtos.Appointment;
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
    public class AppointmentController : ControllerBase
    {
        private readonly AppointmentRepository _appointmentRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public AppointmentController(
            IMapper mapper,
            AppointmentRepository appointmentRepository,
            UserManager<AppUser> userManager
        )
        {
            _appointmentRepository = appointmentRepository;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet("/vet")]
        public async Task<ActionResult<List<AppointmentDto>>> GetAppointmentsForVet(
            AppointmentDto appointmentDto
        )
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);

            var appointments = await _appointmentRepository.GetAppointmentsForVet(user.Id);

            if (appointments is null)
                return NotFound();

            var appDtos = _mapper.Map<List<AppointmentDto>>(appointments);

            return Ok(appDtos);
        }

        [HttpGet("/owner")]
        public async Task<ActionResult<List<AppointmentDto>>> GetAppointmentsForOwner(
            AppointmentDto appointmentDto
        )
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);

            var appointments = await _appointmentRepository.GetAppointmentsForVet(user.Id);

            if (appointments is null)
                return NotFound();

            var appDtos = _mapper.Map<List<AppointmentDto>>(appointments);

            return Ok(appDtos);
        }

        [HttpPost]
        public async Task<ActionResult> RequestAppointment(
            RequestAppointmentDto requestAppointmentDto
        )
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);

            var appointment = new Appointment
            {
                // TODO add real data of owner and vet
                OwnerId = user.Id,
                VetId = user.Id,
                Status = AppointmentStatus.Requested,
                PetId = 1,
                ServiceId = requestAppointmentDto.ServiceId,
                Date = requestAppointmentDto.Date,
                Motive = requestAppointmentDto.Motive
            };

            _appointmentRepository.CreateAppointment(appointment);

            if (await _appointmentRepository.Complete() is false)
                return BadRequest();

            return Ok(_mapper.Map<AppointmentDto>(appointment));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> AcceptOrUpdateAppointment(
            UpdateAppointmentDto updateAppointmentDto,
            int id
        )
        {
            var appointment = await _appointmentRepository.GetAppointment(id);

            if (appointment is null)
                return NotFound("Appointment not found");

            if (appointment.Date == updateAppointmentDto.Date)
            {
                appointment.Status = AppointmentStatus.Accepted;
                _appointmentRepository.UpdateAppointment(appointment);
                if (await _appointmentRepository.Complete())
                    return Ok("Appointment accepted");
                return BadRequest();
            }

            appointment.Status = AppointmentStatus.DateChangeRequested;
            appointment.Date = updateAppointmentDto.Date;
            _appointmentRepository.UpdateAppointment(appointment);
            if (await _appointmentRepository.Complete())
                return Ok("Date changed");

            return BadRequest();
        }

        [HttpDelete("{id}")]
        // TODO only admins or moderators can delete this
        public async Task<ActionResult> DeleteAppointment(int id)
        {
            var appointment = await _appointmentRepository.GetAppointment(id);

            if (appointment is null)
                return NotFound();

            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);

            if (appointment.OwnerId != user.Id || appointment.VetId != user.Id)
                return BadRequest();

            var result = _appointmentRepository.DeleteAppointment(appointment);

            if (!result)
                return BadRequest("There was a problem deleting the adoption");

            return NoContent();
        }
    }
}
