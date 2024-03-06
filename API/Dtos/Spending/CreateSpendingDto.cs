using API.Entities;

namespace API.Dtos.Spending
{
    public class CreateSpendingDto
    {
        public int PetId { get; set; }
        public SpendingCategory Category { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}

