using API.Entities;
using API.Entities.Identity;
using API.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext
        : IdentityDbContext<
            AppUser,
            AppRole,
            string,
            IdentityUserClaim<string>,
            AppUserRole,
            IdentityUserLogin<string>,
            IdentityRoleClaim<string>,
            IdentityUserToken<string>
        >
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options) { }

        public DbSet<Pet> Pets { get; set; }
        public DbSet<MedicalHistory> MedicalHistories { get; set; }
        public DbSet<Adoption> Adoptions { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Vet> Vets { get; set; }
        public DbSet<VetService> VetServices { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Connection> Connections { get; set; }
        public DbSet<Spending> Spendings { get; set; }
        public DbSet<Vaccine> Vaccines { get; set; }
        public DbSet<PetVaccine> PetVaccines { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<AppUser>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
            modelBuilder.Entity<AppRole>().Property(e => e.Id).ValueGeneratedOnAdd();
            modelBuilder
                .Entity<AppRole>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

            modelBuilder.Entity<Pet>().ToTable("Pet").HasKey(p => p.Id);
            modelBuilder.Entity<Service>().ToTable("Service").HasKey(s => s.Id);
            modelBuilder.Entity<Pet>().HasOne(p => p.Owner);

            modelBuilder.Entity<MedicalHistory>().ToTable("MedicalHistory").HasKey(p => p.Id);
            modelBuilder.Entity<MedicalHistory>().HasOne(p => p.Pet);

            modelBuilder.Entity<Adoption>().ToTable("Adoption").HasKey(p => p.Id);
            modelBuilder.Entity<Adoption>().HasOne(a => a.AppUser);

            modelBuilder
                .Entity<VetService>()
                .HasOne(vs => vs.Vet)
                .WithMany(v => v.VetServices)
                .HasForeignKey(vs => vs.VetId);
            modelBuilder
                .Entity<VetService>()
                .HasOne(vs => vs.Service)
                .WithMany(s => s.VetServices)
                .HasForeignKey(vs => vs.ServiceId);
            modelBuilder
                .Entity<VetService>()
                .HasIndex(vs => new { vs.VetId, vs.ServiceId })
                .IsUnique();

            modelBuilder.Entity<Appointment>().ToTable("Appointment").HasKey(a => a.Id);
            modelBuilder
                .Entity<Appointment>()
                .HasOne(a => a.Service)
                .WithMany(s => s.Appointments)
                .HasForeignKey(a => a.ServiceId);
            modelBuilder
                .Entity<Appointment>()
                .HasOne(a => a.Owner)
                .WithMany(u => u.OwnerAppointments)
                .HasForeignKey(a => a.OwnerId);
            modelBuilder
                .Entity<Appointment>()
                .HasOne(a => a.Vet)
                .WithMany(u => u.VetAppointments)
                .HasForeignKey(a => a.VetId);
            modelBuilder.Entity<Appointment>().HasOne(a => a.Pet);

            modelBuilder.Entity<Review>().ToTable("Review").HasKey(r => r.Id);
            modelBuilder
                .Entity<Review>()
                .HasOne(a => a.Owner)
                .WithMany(u => u.OwnerReviews)
                .HasForeignKey(a => a.OwnerId);
            modelBuilder
                .Entity<Review>()
                .HasOne(a => a.Vet)
                .WithMany(u => u.VetReviews)
                .HasForeignKey(a => a.VetId);

            modelBuilder
                .Entity<Message>()
                .HasOne(u => u.Recipient)
                .WithMany(m => m.MessagesReceived)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder
                .Entity<Message>()
                .HasOne(u => u.Sender)
                .WithMany(m => m.MessagesSent)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<AppUser>()
                .HasMany(u => u.Spendings)
                .WithOne(s => s.Owner)
                .HasForeignKey(s => s.OwnerId);

            modelBuilder
                .Entity<Pet>()
                .HasMany(p => p.Spendings)
                .WithOne(s => s.Pet)
                .HasForeignKey(s => s.PetId)
                .IsRequired(false);
            modelBuilder
                .Entity<Pet>()
                .HasMany(p => p.Vaccines)
                .WithMany(p => p.Pets)
                .UsingEntity<PetVaccine>();

            modelBuilder.Seed();
        }
    }
}
