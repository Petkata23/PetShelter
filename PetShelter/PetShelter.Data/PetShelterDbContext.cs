using Microsoft.EntityFrameworkCore;
using PetShelter.Data.Entities;
using PetShelter.Shared;
using PetShelter.Shared.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShelter.Data
{
    public class PetShelterDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Breed> Breeds { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Shelter> Shelters { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Vaccine> Vaccines { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<PetType> User { get; set; }
        public DbSet<PetVaccine> PetVaccines { get; set; }

        
        public PetShelterDbContext(DbContextOptions<PetShelterDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Breed>()
                .HasMany(b => b.Pets)
                .WithOne(p => p.Breed)
                .HasForeignKey(p => p.BreedId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.AdoptedPets)
                .WithOne(p => p.Adopter)
                .HasForeignKey(p => p.AdopterId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
               .HasMany(u => u.GivenPets)
               .WithOne(u => u.Giver)
               .HasForeignKey(p => p.GiverId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Shelter>()
                .HasOne(a => a.Location)
                .WithOne(a => a.Shelter)
                .HasForeignKey<Location>(c => c.ShelterId);

            foreach (var role in Enum.GetValues(typeof(UserRole)).Cast<UserRole>())
            {
                modelBuilder.Entity<Role>().HasData(new Role { Id = (int)role, Name = role.ToString() });
            }

            modelBuilder.Entity<User>()
                .HasData(new User
                {
                    Id = 1,
                    Username = "admin",
                    RoleId = (int)UserRole.Admin,
                    FirstName = "Admin",
                    LastName = "User",
                    Password = PasswordHasher.HashPassword("string")
                });

        }


    }
}
