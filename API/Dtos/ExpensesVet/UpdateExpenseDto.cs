using API.Entities;

namespace API.Dtos
{
    public class UpdateExpenseDto
    {
        public int Id { get; set; }
        public ExpenseCategory Category { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; } = DateTime.Today;
        public string VetId { get; set; }
        public int Amount { get; set; }
        public bool Extra { get; set; }
    }
}
