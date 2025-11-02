using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TooliRent.Models;

namespace TooliRent.Infrastructure.Data
{
    public class TooliRentContext : DbContext
    {
        public TooliRentContext(DbContextOptions<TooliRentContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Tool> Tools { get; set; }
        public DbSet<Category> Categorys { get; set; }
        public DbSet<OrderDeatils> OrderDeatils { get; set; }
        //public DbSet<Reservation> Reservations { get; set; }
        //public DbSet<ReservationTool> ReservationTools { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ConfigureRelations(modelBuilder);

            SeedData(modelBuilder);
        }
        private void ConfigureRelations(ModelBuilder modelBuilder)
        {
            // User -> OrderDetails
            modelBuilder.Entity<User>()
                .HasMany(u => u.OrderDetails)
                .WithOne(o => o.User)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Category -> Tools
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Tools)
                .WithOne(t => t.Category)
                .HasForeignKey(t => t.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Tool -> OrderDetails
            modelBuilder.Entity<Tool>()
                .HasMany(t => t.OrderDetails)
                .WithOne(o => o.Tool)
                .HasForeignKey(o => o.ToolId)
                .OnDelete(DeleteBehavior.Restrict);

            // Decimal precision för priser
            modelBuilder.Entity<Tool>()
                .Property(t => t.PricePerDay)
                .HasPrecision(10, 2);

            modelBuilder.Entity<OrderDeatils>()
                .Property(o => o.TotalPrice)
                .HasPrecision(10, 2);

            modelBuilder.Entity<OrderDeatils>()
                .Property(o => o.LateFee)
                .HasPrecision(10, 2);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed Users
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    FirstName = "Leon",
                    LastName = "Johansson",
                    Email = "Leon.Johanssonsens@example.com",
                    PasswordHash = "Adminpower",
                    Role = "Admin"
                },
                new User
                {
                    Id = 2,
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "DoeTheJohn@example.com",
                    PasswordHash = "password123",
                    Role = "Member"
                },
                new User
                {
                    Id = 3,
                    FirstName = "Petter",
                    LastName = "Boström",
                    Email = "StarTrekFan@example.com",
                    PasswordHash = "Maythe4",
                    Role = "Member"
                }
            );

            // Seed Categories
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    Name = "Elverktyg",
                    Description = "Elektriska verktyg för byggarbete och hantverk"
                },
                new Category
                {
                    Id = 2,
                    Name = "Handverktyg",
                    Description = "Traditionella handverktyg för precision och detaljarbete"
                },
                new Category
                {
                    Id = 3,
                    Name = "Mätverktyg",
                    Description = "Verktyg för mätning och märkning"
                },
                new Category
                {
                    Id = 4,
                    Name = "Säkerhetsutrustning",
                    Description = "Skyddsutrustning för säkert arbete"
                }
            );

            // Seed Tools
            modelBuilder.Entity<Tool>().HasData(
                // Elverktyg
                new Tool
                {
                    Id = 1,
                    Name = "Slagborrmaskin",
                    Brand = "Bosch",
                    Model = "GSB 13 RE",
                    Description = "Professionell slagborrmaskin för betong och murning",
                    PricePerDay = 45.00m,
                    IsAvailable = true,
                    SerialNumber = "BOSCH-001",
                    CategoryId = 1
                },
                new Tool
                {
                    Id = 2,
                    Name = "Vinkelslip",
                    Brand = "Makita",
                    Model = "GA5030",
                    Description = "125mm vinkelslip för kapning och slipning",
                    PricePerDay = 35.00m,
                    IsAvailable = true,
                    SerialNumber = "MAKITA-002",
                    CategoryId = 1
                },
                new Tool
                {
                    Id = 3,
                    Name = "Sticksåg",
                    Brand = "Festool",
                    Model = "PS 420 EBQ",
                    Description = "Precisionsticksåg med pendelfunktion",
                    PricePerDay = 55.00m,
                    IsAvailable = true,
                    SerialNumber = "FESTOOL-003",
                    CategoryId = 1
                },
                // Handverktyg
                new Tool
                {
                    Id = 4,
                    Name = "Hammare",
                    Brand = "Stanley",
                    Model = "STHT0-51309",
                    Description = "Kluvspark 450g med glasfiberhandtag",
                    PricePerDay = 15.00m,
                    IsAvailable = true,
                    SerialNumber = "STANLEY-004",
                    CategoryId = 2
                },
                new Tool
                {
                    Id = 5,
                    Name = "Skruvmejselset",
                    Brand = "Wera",
                    Model = "Kraftform Plus",
                    Description = "Set med 6 isolerade skruvmejslar",
                    PricePerDay = 20.00m,
                    IsAvailable = true,
                    SerialNumber = "WERA-005",
                    CategoryId = 2
                },
                // Mätverktyg
                new Tool
                {
                    Id = 6,
                    Name = "Lasermätare",
                    Brand = "Leica",
                    Model = "DISTO D2",
                    Description = "Precision laserdistansmätare upp till 100m",
                    PricePerDay = 40.00m,
                    IsAvailable = true,
                    SerialNumber = "LEICA-006",
                    CategoryId = 3
                },
                new Tool
                {
                    Id = 7,
                    Name = "Vattenpass",
                    Brand = "Stabila",
                    Model = "70-2",
                    Description = "Aluminium vattenpass 60cm med 3 libeller",
                    PricePerDay = 25.00m,
                    IsAvailable = true,
                    SerialNumber = "STABILA-007",
                    CategoryId = 3
                },
                // Säkerhetsutrustning
                new Tool
                {
                    Id = 8,
                    Name = "Skyddshjälm",
                    Brand = "3M",
                    Model = "SecureFit X5000",
                    Description = "Ventilerad skyddshjälm med justerbart huvudband",
                    PricePerDay = 10.00m,
                    IsAvailable = true,
                    SerialNumber = "3M-008",
                    CategoryId = 4
                },
                new Tool
                {
                    Id = 9,
                    Name = "Skyddsglasögon",
                    Brand = "Uvex",
                    Model = "i-vo",
                    Description = "Skyddsglasögon med anti-imma coating",
                    PricePerDay = 8.00m,
                    IsAvailable = false,
                    SerialNumber = "UVEX-009",
                    CategoryId = 4
                }
            );

            // Seed OrderDetails
            modelBuilder.Entity<OrderDeatils>().HasData(
                new OrderDeatils
                {
                    Id = 1,
                    Date2Hire = new DateTime(2024, 9, 1),
                    Date2Return = new DateTime(2024, 9, 4),
                    Status = "Returned",
                    TotalPrice = 105.00m,
                    LateFee = 0.00m,
                    CheckedOutAt = new DateTime(2024, 9, 1),
                    ReturnedAt = new DateTime(2024, 9, 4),
                    CreatedAt = new DateTime(2024, 9, 1),
                    UpdatedAt = new DateTime(2024, 9, 4),
                    UserId = 3,
                    ToolId = 2,
                    ReservationId = null
                }
            );
        }
    }
}
