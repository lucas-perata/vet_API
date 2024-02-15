using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities.Identity;

namespace API.Entities
{
    public class Service
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public float Price { get; set; }
    public int Duration { get; set; }
    public ICollection<VetService> VetServices { get; set; }

}
}