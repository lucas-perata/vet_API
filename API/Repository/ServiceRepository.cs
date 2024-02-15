using API.Data;
using API.Entities;
using API.Entities.Identity;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class ServiceRepository : IServiceInterface
    {
        private readonly DataContext _context;
        public ServiceRepository(DataContext context)
        {
            _context = context; 
        }
        public async Task<bool> VetServiceExistsAsync(string vetId, int serviceId)
        {
            return await _context.VetServices.AnyAsync(vs => vs.VetId == vetId && vs.ServiceId == serviceId);
        }

        public void AddService(VetService vetService)
        {
            _context.VetServices.Add(vetService);
        }

        public async Task<List<Service>> GetServicesForVet(string vetId)
        {
           return await _context.VetServices
                            .Where(vs => vs.VetId == vetId && vs.IsOffered)
                            .Select(vs => vs.Service)
                            .ToListAsync(); 
        }

        public async Task<bool> Complete()
        {
            return await _context.SaveChangesAsync() > 0; 
        }

        

        public bool DeleteService(VetService vetService)
        {
            _context.Remove(vetService);
            return _context.SaveChanges() > 0;
        }

        public async Task<Service> GetService(int serviceId)
        {
            return await _context.Services.FirstOrDefaultAsync(s => s.Id == serviceId);
        }

        public async Task<VetService> GetVetServiceRelation(int serviceId)
        {
            return await _context.VetServices
                    .Where(vs => vs.ServiceId == serviceId)
                    .FirstOrDefaultAsync();
        }

        public async Task<List<Service>> GetServices()
        {
            return await _context.Services.ToListAsync();
        }
    }
}