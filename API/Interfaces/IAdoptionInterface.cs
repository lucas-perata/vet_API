using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.Interfaces
{
    public interface IAdoptionInterface
    {
        Task<List<Adoption>> GetAdoptions(); 
        Task<Adoption> GetAdoption(int id); 
        void CreateAdoptionWithPetAsync(Adoption adoption); 
        Task<bool> Complete(); 
        void UpdateAdoption(Adoption adoption);
        bool DeleteAdoption(Adoption adoption); 
    }
}