using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.Dtos.Adoption
{
    public class CreateAdoptionDto
    {
        public int PetId { get; set; }
        public string AppUserId { get; set; }
        public bool IsNeutered { get; set; }
        public bool IsVaccinated { get; set; }
        public bool IsDeworm {get; set;}
    }
}