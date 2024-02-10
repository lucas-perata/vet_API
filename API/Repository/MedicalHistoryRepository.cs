using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class MedicalHistoryRepository : IMedicalHistoryInterface
    {   
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public MedicalHistoryRepository(DataContext context, IMapper mapper)
        {
            _context = context; 
            _mapper = mapper;
        }
        public void AddMedicalHistory(MedicalHistory medicalHistory)
        {
            _context.MedicalHistories.Add(medicalHistory); 
        }

        public async Task<bool> Complete()
        {
            return await _context.SaveChangesAsync() > 0; 
        }

        public bool DeleteMedicalHistory(MedicalHistory medicalHistory)
        {
            _context.Remove(medicalHistory);
            return _context.SaveChanges() > 0; 
        }

        public async Task<List<MedicalHistory>> GetAllMedicalHistoryForPet(int petId)
        {
            return await _context.MedicalHistories
                            .Where(m => m.PetId == petId)
                            .ToListAsync();
        }

        public async Task<MedicalHistory> GetMedicalHistory(int id)
        {
            return await _context.MedicalHistories.FindAsync(id); 
        }

        public void UpdateMedicalHistory(MedicalHistory medicalHistory)
        {
            _context.Entry(medicalHistory).State = EntityState.Modified;
        }
    }
}