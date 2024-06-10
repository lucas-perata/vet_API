using Microsoft.AspNetCore.Identity;

namespace API.Entities.Identity
{
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; }
        public List<Photo> Photos { get; set; } = new();
        public ICollection<Pet> Pets { get; set; }
        public ICollection<VetService> VetServices { get; set; }
        public ICollection<Appointment> OwnerAppointments { get; set; }
        public ICollection<Appointment> VetAppointments { get; set; }
        public ICollection<Review> OwnerReviews { get; set; }
        public ICollection<Review> VetReviews { get; set; }
        public List<Message> MessagesSent { get; set; }
        public List<Message> MessagesReceived { get; set; }
        public ICollection<Spending> Spendings { get; set; }
        public ICollection<ExpensesVet> ExpensesVet { get; set; }
        public ICollection<AppUserRole> UserRoles { get; set; }
    }
}
