using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos.Adoption
{
    public class CreateAdoptionWithPetDto
    {
        public CreateAdoptionDto CreateAdoptionDto { get; set; }
        public CreatePetDto CreatePetDto { get; set; }
    }
}