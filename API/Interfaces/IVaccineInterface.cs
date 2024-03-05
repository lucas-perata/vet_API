using API.Entities;

namespace API.Interfaces
{
    public interface IVaccineInterface
    {
        Task<List<Vaccine>> GetVaccinesForPet(int petId);
        Task<List<Vaccine>> GetVaccines();
        Task<Vaccine> GetVaccine(int vaccineId);
        void AddVaccine(Vaccine vaccine);
        void AddPetVaccine(PetVaccine petVaccine);
        Task<bool> Complete();
        bool DeletePetVaccine(PetVaccine petVaccine);
        void UpdateVaccine(Vaccine vaccine);
        bool DeleteVaccine(Vaccine vaccine);
        Task<bool> PetVaccineExistsAsync(int petId, int vaccineId);
        Task<PetVaccine> GetPetVaccineRelation(int vaccineId);
    }
}
