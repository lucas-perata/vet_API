using API.Entities.Identity;

namespace API.Entities
{
    public class ExpensesVet
    {
        public int Id { get; set; }
        public ExpenseCategory Category { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; } = DateTime.Today;
        public string VetId { get; set; }
        public AppUser Vet { get; set; }
        public int Amount { get; set; }
    }

    public enum ExpenseCategory
    {
        Alquiler,
        Impuestos,
        Servicios,
        Extraordinarios,
        Otros
    }
}
