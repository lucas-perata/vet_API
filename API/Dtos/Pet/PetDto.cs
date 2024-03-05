using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos.Photo;
using API.Entities;

namespace API.Dtos
{
    public class PetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Breed { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Color { get; set; }
        public string Gender { get; set; }
        public int Weight { get; set; }
        public bool IsNeutered { get; set; }
        public string OwnerId { get; set; }
        public List<PetPhotoDto> PetPhoto { get; set; }

    }
}