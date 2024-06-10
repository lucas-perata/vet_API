using API.Entities;

namespace API.Dtos
{
    public class ExpensesVetDto
    {
        public int Id { get; set; }
        public ExpenseCategory Category { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; } = DateTime.Today;
        public int Amount { get; set; }
        public bool Extra { get; set; }
    }
}
