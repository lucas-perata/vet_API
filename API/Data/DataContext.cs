using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.Entities.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Pet> Pets { get; set; }
        public DbSet<MedicalHistory> MedicalHistories {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
        }
    }
}