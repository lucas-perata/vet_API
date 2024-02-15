using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities.Identity;

namespace API.Entities
{
    public class Appointment
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string OwnerId { get; set; }
        public AppUser Owner { get; set; }
        public string VetId { get; set; }
        public AppUser Vet { get; set; }
        public int PetId { get; set; }
        public Pet Pet { get; set; }
    }
}