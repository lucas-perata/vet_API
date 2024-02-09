using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Entities;

namespace API.Interfaces
{
    public interface IPetInterface
    {
        Task<List<Pet>> GetPets();
        Task<PetDto> GetPet(int id);
    }
}