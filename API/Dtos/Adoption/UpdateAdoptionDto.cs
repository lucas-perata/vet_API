using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos.Adoption
{
    public class UpdateAdoptionDto
    {
        public bool IsNeutered { get; set; }
        public bool IsVaccinated { get; set; }
        public bool IsDeworm {get; set;}
    }
}