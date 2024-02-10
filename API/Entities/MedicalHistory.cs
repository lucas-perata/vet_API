using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class MedicalHistory
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }   
        public string Description { get; set; }
        public string Treatment { get; set; }
        public string VetName { get; set; }
        public int PetId {get; set;}
        public Pet Pet { get; set; }
    }
}