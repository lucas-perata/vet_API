using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities.Identity;

namespace API.Entities
{
    public class Adoption
    {
        public int Id { get; set; }
        public int PetId { get; set; }
        public Pet Pet { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public bool IsNeutered { get; set; }
        public bool IsVaccinated { get; set; }
        public bool IsDeworm {get; set;}
        public string Status { get; set; }
        public ICollection<MedicalHistory> MedicalHistories { get; set; }
    }
}