using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using MotoRentalApp.Domain.Entities;
using MotoRentalApp.Domain.Enums;

namespace MotoRentalApp.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Rent> Rents { get; set; }
        public DbSet<Fine> Fines { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<DeliveryPerson> DeliveryPersons { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            optionsBuilder.ConfigureWarnings(warnings =>
                warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.Entity<Customer>()
                .Property(c => c.Id)
                .ValueGeneratedOnAdd()  
                .UseIdentityColumn();   

            modelBuilder.Entity<Rent>()
                .Property(r => r.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            modelBuilder.Entity<Vehicle>()
                .Property(v => v.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            modelBuilder.Entity<User>()
                .Property(u => u.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            modelBuilder.Entity<Payment>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            modelBuilder.Entity<Fine>()
                .Property(f => f.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            modelBuilder.Entity<User>().HasData(
                    new User
                    {
                        Id = 1,
                        Username = "admin",
                        Password = "D41D8CD98F00B204E9800998ECF8427E",
                        Email = "admin@admin.com",
                        FullName = "Admin User",
                        Role = UserRole.Admin,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    },
                    new User
                    {
                        Id = 2,
                        Username = "entregador",
                        Password = "3DD6B9265FF18F31DC30DF59304B0CA7",
                        Email = "entregador@entregador.com",
                        FullName = "Entregador User",
                        Role = UserRole.Entregador,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    }
                );
            
        }
    }
}