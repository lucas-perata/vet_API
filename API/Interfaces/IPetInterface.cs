using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Interfaces
{
    public interface IPetInterface
    {
        Task<List<Pet>> GetPets();
        Task<List<Pet>> GetPetsForOwner(string ownerId);
        Task<Pet> GetPet(int id); 
        void AddPet(Pet pet); 
        Task<bool> Complete(); 
        void UpdatePet(Pet pet); 
        bool DeletePet(Pet pet);
    }
}