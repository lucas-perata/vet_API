using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos.Adoption
{
    public class AdoptionWithPetDto
    {
        public CreateAdoptionDto Adoption { get; set; }
        public PetDto Pet { get; set; }
    }
}