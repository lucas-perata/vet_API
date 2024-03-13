using API.Entities.Identity;

namespace API.Entities
{
    public class Spending
    {
        public int Id { get; set; }
        public int? PetId { get; set; }
        public Pet? Pet { get; set; }
        public SpendingCategory Category { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; } = DateTime.Today;
        public string OwnerId { get; set; }
        public AppUser Owner { get; set; }
    }

    public enum SpendingCategory
    {
        Comida,
        Veterinario,
        Juguetes,
        Aseo,
        Medicamentos,
        Otros
    }
}

