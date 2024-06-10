using API.Entities;

namespace API.Dtos
{
    public class CreateExpenseDto
    {
        public ExpenseCategory Category { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; } = DateTime.Today;
        public int Amount { get; set; }
    }
}
