using API.Entities;
using API.Entities.Identity;
using API.Extensions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Pet> Pets { get; set; }
        public DbSet<MedicalHistory> MedicalHistories {get; set;}
        public DbSet<Adoption> Adoptions {get; set;}
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Vet> Vets { get; set; }
        public DbSet<VetService> VetServices {get; set;}
        public DbSet<Service> Services { get; set; }
        public DbSet<Appointment> Appointments {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Pet>()
                .ToTable("Pet")
                .HasKey(p => p.Id);
            modelBuilder.Entity<Service>()
                .ToTable("Service")
                .HasKey(s => s.Id);
            modelBuilder.Entity<Pet>()
                .HasOne(p => p.Owner);
            modelBuilder.Entity<MedicalHistory>()
                .ToTable("MedicalHistory")  
                .HasKey(p => p.Id); 
            modelBuilder.Entity<MedicalHistory>()
                .HasOne(p => p.Pet); 
            modelBuilder.Entity<Adoption>()
                .ToTable("Adoption")
                .HasKey(p => p.Id); 
            modelBuilder.Entity<Adoption>()
                .HasOne(p => p.Pet);
            modelBuilder.Entity<Adoption>() 
                .HasOne(a => a.AppUser); 
             modelBuilder.Entity<VetService>()
                .HasOne(vs => vs.Vet)
                .WithMany(v => v.VetServices)
                .HasForeignKey(vs => vs.VetId);
            modelBuilder.Entity<VetService>()
                .HasOne(vs => vs.Service)
                .WithMany(s => s.VetServices)
                .HasForeignKey(vs => vs.ServiceId);
            modelBuilder.Entity<VetService>()
                .HasIndex(vs => new { vs.VetId, vs.ServiceId })
                .IsUnique();
            modelBuilder.Entity<Appointment>()
                .ToTable("Appointment")
                .HasKey(a => a.Id); 
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Service)
                .WithMany(s => s.Appointments)
                .HasForeignKey(a => a.ServiceId); 
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Owner)
                .WithMany(u => u.OwnerAppointments)
                .HasForeignKey(a => a.OwnerId);
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Vet)
                .WithMany(u => u.VetAppointments)
                .HasForeignKey(a => a.VetId);
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Pet);
                
            modelBuilder.Seed();
        }
    }
}