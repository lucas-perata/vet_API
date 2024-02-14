using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities.Identity
{
    public class VetService
    {
        public int Id { get; set; }
        public string VetId { get; set; }
        public AppUser Vet { get; set; }

        public int ServiceId { get; set; }
        public Service Service { get; set; }

        public bool IsOffered { get; set; }
    }
}