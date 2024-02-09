using API.Data;
using API.Dtos;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class IPetRepository : IPetInterface
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public IPetRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void AddPet(Pet pet)
        {
           _context.Pets.Add(pet);
        }

        public async Task<bool> Complete()
        {
             return await _context.SaveChangesAsync() > 0; 
        }

        public async Task<PetDto> GetPet(int id)
        {
           var pet = await _context.Pets.FindAsync(id);
            return new PetDto
            {
                Id = pet.Id,
                Name = pet.Name, 
                Breed = pet.Breed,
                DateOfBirth = pet.DateOfBirth,
                Color = pet.Color,
                OwnerId = pet.OwnerId
            }; 
        }

        public Task<List<Pet>> GetPets()
        {
            throw new NotImplementedException();
        }
    }
}