using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos.Service
{
    public class AddVetServiceDto
    {
        public string VetId { get; set; }
        public int ServiceId { get; set; }
        public bool IsOffered { get; set; }
    }
}