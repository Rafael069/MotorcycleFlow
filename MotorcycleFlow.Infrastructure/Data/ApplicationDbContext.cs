using Microsoft.EntityFrameworkCore;
using MotorcycleFlow.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorcycleFlow.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Motorcycle> Motorcycles { get; set; }
        public DbSet<DeliveryPerson> DeliveryPeople { get; set; }
        public DbSet<Rental> Rentals { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure RELATIONSHIPS

            
         modelBuilder.Entity<Rental>()
            .HasOne(r => r.DeliveryPerson)
            .WithMany(d => d.Rentals)
            .HasForeignKey(r => r.DeliveryPersonId)
            .OnDelete(DeleteBehavior.Restrict);

         modelBuilder.Entity<Rental>()
            .HasOne(r => r.Motorcycle)
            .WithMany(m => m.Rental)
            .HasForeignKey(r => r.MotorcycleId)
            .OnDelete(DeleteBehavior.Restrict);

            // Configure table names
            modelBuilder.Entity<Motorcycle>().ToTable("Motorcycles");
            modelBuilder.Entity<DeliveryPerson>().ToTable("DeliveryPersons");
            modelBuilder.Entity<Rental>().ToTable("Rentals");

        }
    }
}
