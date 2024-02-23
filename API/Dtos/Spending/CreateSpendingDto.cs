using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.Dtos.Spending
{
    public class CreateSpendingDto
    {
        public int Id { get; set; }
        public int? PetId { get; set; }
        public SpendingCategory Category { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string OwnerId { get; set; }
    }
}