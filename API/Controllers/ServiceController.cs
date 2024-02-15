using System.Collections;
using System.Security.Claims;
using API.Dtos.Service;
using API.Entities;
using API.Entities.Identity;
using API.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceController : ControllerBase
    {
        private readonly ServiceRepository _serviceRepository; 
        private readonly UserManager<AppUser> _userManager;
        public ServiceController(ServiceRepository serviceRepository, UserManager<AppUser> userManager)
        {
            _serviceRepository = serviceRepository; 
            _userManager = userManager; 
        }

        [HttpGet("get-services-vet")]
        public async Task<ActionResult<IList>> GetServicesForVet()
        {
            var email = User.FindFirstValue(ClaimTypes.Email); 
            var user = await _userManager.FindByEmailAsync(email);

            var services = await _serviceRepository.GetServicesForVet(user.Id);

            if(services is null) return NotFound(); 

            return services; 
        }

        [HttpGet("get-all-services")]
        public async Task<ActionResult<IList>> GetServices()
        {
            var services = await _serviceRepository.GetServices(); 

            if(services is null) return NotFound(); 

            return services; 
        }


        [HttpGet("{serviceId}")]
        public async Task<ActionResult<Service>> GetService(int serviceId)
        {
            var service = await _serviceRepository.GetService(serviceId);

            return service;
        }


        [HttpPost]
        public async Task<IActionResult> AddService(int serviceId)
        {
            var email = User.FindFirstValue(ClaimTypes.Email); 
            var user = await _userManager.FindByEmailAsync(email);

            if (await _serviceRepository.VetServiceExistsAsync(user.Id, serviceId))
            {
                return BadRequest("This service is already offered by the vet");
            }

            var service = new VetService
            {
                ServiceId = serviceId, 
                VetId = user.Id,
                IsOffered = true
            }; 

            _serviceRepository.AddService(service); 

            if(await _serviceRepository.Complete())
            {
                var serviceVet = new AddVetServiceDto
                {
                    ServiceId = service.ServiceId,
                    VetId = service.VetId,
                    IsOffered = service.IsOffered
                };

                return Ok(serviceVet);
            } 

            return BadRequest("There was a problem adding this service"); 
        }   

        [HttpDelete("{vsId}")]
        public async Task<ActionResult> DeleteService(int vsId)
        {
            var service = await _serviceRepository.GetVetServiceRelation(vsId); 

            if(service is null) return NotFound("Relation not found"); 

            var result = _serviceRepository.DeleteService(service);

            if(!result) return BadRequest("There was a problem deleting the relation");

            return NoContent();
        }
        
        
    }
}