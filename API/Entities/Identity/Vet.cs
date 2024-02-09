using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities.Identity
{
    public class Vet
    {
        public string Id { get; set; }
        public AppUser User { get; set; }
    }
}