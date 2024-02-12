using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class AdoptionRepository : IAdoptionInterface
    {
        private readonly DataContext _context; 
        public AdoptionRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<bool> Complete()
        {
            return await _context.SaveChangesAsync() > 0; 
        }

        public void CreateAdoptionWithPetAsync(Adoption adoption)
        {
            _context.Adoptions.Add(adoption); 
        }

        public bool DeleteAdoption(Adoption adoption)
        {
            _context.Remove(adoption); 
            return _context.SaveChanges() > 0; 
        }

        public async Task<Adoption> GetAdoption(int id)
        {
            return await _context.Adoptions
                            .Include(p => p.Pet)
                            .FirstOrDefaultAsync(a => a.Id == id);    
        }

        public async Task<List<Adoption>> GetAdoptions()
        {
            return await _context.Adoptions
                            .Include(p => p.Pet)
                            .ToListAsync(); 
        }

        public void UpdateAdoption(Adoption adoption)
        {
            _context.Entry(adoption).State = EntityState.Modified;
        }
    }
}