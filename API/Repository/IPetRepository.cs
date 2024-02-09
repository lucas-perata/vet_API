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

        public bool DeletePet(Pet pet)
        {
            _context.Remove(pet);
            return _context.SaveChanges() > 0;
        }

        public async Task<Pet> GetPet(int id)
        {
           return await _context.Pets.FindAsync(id);
        }

        public Task<List<Pet>> GetPets()
        {
            throw new NotImplementedException();
        }

        public void UpdatePet(Pet pet)
        {
            _context.Entry(pet).State = EntityState.Modified;
        }
    }
}