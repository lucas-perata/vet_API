using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class ModelBuilderExtensions
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Service>().HasData(
            new Service { Id = 1, Name = "Service 1", Description = "Description 1", Price = 100.0f, Duration = 60 },
            new Service { Id = 2, Name = "Service 2", Description = "Description 2", Price = 200.0f, Duration = 120 },
            new Service { Id = 3, Name = "Service 3", Description = "Description 3", Price = 300.0f, Duration = 180 }
        );
    }
}
}