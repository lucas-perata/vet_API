using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos.Adoption;
using API.Entities;
using API.Helpers;

namespace API.Interfaces
{
    public interface IAdoptionInterface
    {
        Task<PagedList<AdoptionDto>> GetAdoptions(UserParams userParams); 
        Task<Adoption> GetAdoption(int id); 
        void CreateAdoptionWithPetAsync(Adoption adoption); 
        Task<bool> Complete(); 
        void UpdateAdoption(Adoption adoption);
        bool DeleteAdoption(Adoption adoption); 
    }
}