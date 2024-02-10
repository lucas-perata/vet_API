using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.Interfaces
{
    public interface IMedicalHistoryInterface
    {
        Task<List<MedicalHistory>> GetAllMedicalHistoryForPet(int petId);
        Task<MedicalHistory> GetMedicalHistory (int id); 
        void AddMedicalHistory(MedicalHistory medicalHistory);
        void UpdateMedicalHistory(MedicalHistory medicalHistory); 
        bool DeleteMedicalHistory(MedicalHistory medicalHistory);
        Task<bool> Complete();
    }
}