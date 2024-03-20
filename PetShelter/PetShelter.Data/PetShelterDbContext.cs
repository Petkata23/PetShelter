using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShelter.Data
{
    class PetShelterDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Breed> Breeds { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Shelter> Shelters { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Vaccine> Vaccines { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<PetType> Users { get; set; }
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
                .HasMany(a => b.Location)
                .WithOne(a => a.Shelter)
                .HasForeignKey(c => c.BreedId);

        }


    }
}
