using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.Entities.Identity;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Pet>()
                .ToTable("Pet")
                .HasKey(p => p.Id);
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
        }
    }
}