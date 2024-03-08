using API.Data;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class VaccineRepository : IVaccineInterface
    {
        private readonly DataContext _context;

        public VaccineRepository(DataContext context)
        {
            _context = context;
        }

        public void AddVaccine(Vaccine vaccine)
        {
            _context.Vaccines.Add(vaccine);
        }

        public void AddPetVaccine(PetVaccine petVaccine)
        {
            _context.PetVaccines.Add(petVaccine);
        }

        public async Task<List<Vaccine>> GetVaccinesForPet(int petId)
        {
            return await _context
                .PetVaccines.Where(pv => pv.PetId == petId)
                .Select(pv => pv.Vaccine)
                .ToListAsync();
        }

        public async Task<List<Vaccine>> GetMissingVaccinesForPet(int petId)
        {
            var vaccineIdsPetHas = await _context
                .PetVaccines.Where(pv => pv.PetId == petId)
                .Select(pv => pv.VaccineId)
                .ToListAsync();

            return await _context
                .Vaccines.Where(v => !vaccineIdsPetHas.Contains(v.Id))
                .ToListAsync();
        }

        public async Task<List<Vaccine>> GetVaccines()
        {
            return await _context.Vaccines.ToListAsync();
        }

        public async Task<bool> Complete()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public bool DeletePetVaccine(PetVaccine petVaccine)
        {
            _context.Remove(petVaccine);
            return _context.SaveChanges() > 0;
        }

        public async Task<Vaccine> GetVaccine(int vaccineId)
        {
            return await _context.Vaccines.SingleOrDefaultAsync(vaccine => vaccineId == vaccine.Id);
        }

        public void UpdateVaccine(Vaccine vaccine)
        {
            _context.Entry(vaccine).State = EntityState.Modified;
        }

        public bool DeleteVaccine(Vaccine vaccine)
        {
            _context.Remove(vaccine);
            return _context.SaveChanges() > 0;
        }

        public async Task<bool> PetVaccineExistsAsync(int petId, int vaccineId)
        {
            return await _context.PetVaccines.AnyAsync(pv =>
                pv.PetId == petId && pv.VaccineId == vaccineId
            );
        }

        public async Task<PetVaccine> GetPetVaccineRelation(int vaccineId, int petId)
        {
            return await _context
                .PetVaccines.Where(pv => pv.VaccineId == vaccineId)
                .Where(pv => pv.PetId == petId)
                .FirstOrDefaultAsync();
        }
    }
}
