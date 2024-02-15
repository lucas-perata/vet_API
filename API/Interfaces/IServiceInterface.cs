using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.Entities.Identity;

namespace API.Interfaces
{
    public interface IServiceInterface
    {
        Task<List<Service>> GetServicesForVet(string vetId); 
        Task<List<Service>> GetServices();
        Task<Service> GetService(int serviceId);
        Task<VetService> GetVetServiceRelation(int serviceId);
        void AddService(VetService vetService);
        public Task<bool> Complete();
        public bool DeleteService(VetService vetService);
        public Task<bool> VetServiceExistsAsync(string vetId, int serviceId);
    }
}