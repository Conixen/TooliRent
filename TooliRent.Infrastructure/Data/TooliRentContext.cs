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
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<OrderDeatils> OrderDeatils { get; set; }
        public DbSet<ReservationTool> ReservationTools { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ConfigureRelations(modelBuilder);

            SeedData(modelBuilder);
        }
        private void ConfigureRelations(ModelBuilder modelBuilder)
        {
            // User relationer
            modelBuilder.Entity<User>()
                .HasMany(u => u.Reservations)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasMany(u => u.OrderDetails)
                .WithOne(o => o.User)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Category relationer
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Tools)
                .WithOne(t => t.Category)
                .HasForeignKey(t => t.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Tool relationer
            modelBuilder.Entity<Tool>()
                .HasMany(t => t.ReservationTools)
                .WithOne(rt => rt.Tool)
                .HasForeignKey(rt => rt.ToolId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Tool>()
                .HasMany(t => t.OrderDetails)
                .WithOne(o => o.Tool)
                .HasForeignKey(o => o.ToolId)
                .OnDelete(DeleteBehavior.Restrict);

            // Reservation relationer
            modelBuilder.Entity<Reservation>()
                .HasMany(r => r.ReservationTools)
                .WithOne(rt => rt.Reservation)
                .HasForeignKey(rt => rt.ReservationId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Reservation>()
                .HasMany(r => r.OrderDetails)
                .WithOne(o => o.Reservation)
                .HasForeignKey(o => o.ReservationId)
                .OnDelete(DeleteBehavior.SetNull);

            // ReservationTool composite key
            modelBuilder.Entity<ReservationTool>()
                .HasKey(rt => rt.Id);

            modelBuilder.Entity<ReservationTool>()
                .HasIndex(rt => new { rt.ReservationId, rt.ToolId })
                .IsUnique();

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

            modelBuilder.Entity<ReservationTool>()
                .Property(rt => rt.EstimatedPrice)
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
                    PasswordHash = "$2a$11$9Ew4j1GhTpXqMNXlBJ6zU.2Gq8FYzrLX5K9JzE3X7wMdA8V4qZrOm", // "password123"
                    Role = "Admin",

                },
                new User
                {
                    Id = 2,
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "DoeTheJohn@example.com",
                    PasswordHash = "$2a$11$9Ew4j1GhTpXqMNXlBJ6zU.2Gq8FYzrLX5K9JzE3X7wMdA8V4qZrOm", // "password123"
                    Role = "Member",

                },
                new User
                {
                    Id = 3,
                    FirstName = "Petter",
                    LastName = "Boström",
                    Email = "StarTrekFan@example.com",
                    PasswordHash = "$2a$11$9Ew4j1GhTpXqMNXlBJ6zU.2Gq8FYzrLX5K9JzE3X7wMdA8V4qZrOm", // "password123"
                    Role = "Member",

                }
            );
            // Seed Categories
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    Name = "Elverktyg",
                    Description = "Elektriska verktyg för byggarbete och hantverk",

                },
                new Category
                {
                    Id = 2,
                    Name = "Handverktyg",
                    Description = "Traditionella handverktyg för precision och detaljarbete",

                },
                new Category
                {
                    Id = 3,
                    Name = "Mätverktyg",
                    Description = "Verktyg för mätning och märkning",

                },
                new Category
                {
                    Id = 4,
                    Name = "Säkerhetsutrustning",
                    Description = "Skyddsutrustning för säkert arbete",

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
                    Status = "Available",
                    Description = "Professionell slagborrmaskin för betong och murning",
                    PricePerDay = 45.00m,
                    IsAvailable = true,
                    CategoryId = 1,

                },
                new Tool
                {
                    Id = 2,
                    Name = "Vinkelslip",
                    Brand = "Makita",
                    Model = "GA5030",
                    Status = "Available",
                    Description = "125mm vinkelslip för kapning och slipning",
                    PricePerDay = 35.00m,
                    IsAvailable = true,
                    CategoryId = 1,

                },
                new Tool
                {
                    Id = 3,
                    Name = "Sticksåg",
                    Brand = "Festool",
                    Model = "PS 420 EBQ",
                    Status = "Available",
                    Description = "Precisionsticksåg med pendelfunktion",
                    PricePerDay = 55.00m,
                    IsAvailable = true,
                    CategoryId = 1,

                },

                // Handverktyg
                new Tool
                {
                    Id = 4,
                    Name = "Hammare",
                    Brand = "Stanley",
                    Model = "STHT0-51309",
                    Status = "Available",
                    Description = "Kluvspark 450g med glasfiberhandtag",
                    PricePerDay = 15.00m,
                    IsAvailable = true,
                    CategoryId = 2,

                },
                new Tool
                {
                    Id = 5,
                    Name = "Skruvmejselset",
                    Brand = "Wera",
                    Model = "Kraftform Plus",
                    Status = "Available",
                    Description = "Set med 6 isolerade skruvmejslar",
                    PricePerDay = 20.00m,
                    IsAvailable = true,
                    CategoryId = 2,

                },

                // Mätverktyg
                new Tool
                {
                    Id = 6,
                    Name = "Lasermätare",
                    Brand = "Leica",
                    Model = "DISTO D2",
                    Status = "Available",
                    Description = "Precision laserdistansmätare upp till 100m",
                    PricePerDay = 40.00m,
                    IsAvailable = true,
                    CategoryId = 3,

                },
                new Tool
                {
                    Id = 7,
                    Name = "Vattenpass",
                    Brand = "Stabila",
                    Model = "70-2",
                    Status = "Available",
                    Description = "Aluminium vattenpass 60cm med 3 libeller",
                    PricePerDay = 25.00m,
                    IsAvailable = true,
                    CategoryId = 3,

                },

                // Säkerhetsutrustning  
                new Tool
                {
                    Id = 8,
                    Name = "Skyddshjälm",
                    Brand = "3M",
                    Model = "SecureFit X5000",
                    Status = "Available",
                    Description = "Ventilerad skyddshjälm med justerbart huvudband",
                    PricePerDay = 10.00m,
                    IsAvailable = true,
                    CategoryId = 4,

                },
                new Tool
                {
                    Id = 9,
                    Name = "Skyddsglasögon",
                    Brand = "Uvex",
                    Model = "i-vo",
                    Status = "Maintenance",
                    Description = "Skyddsglasögon med anti-imma coating",
                    PricePerDay = 8.00m,
                    IsAvailable = false,
                    CategoryId = 4,

                }
            );
            // Seed ReservationTools
            modelBuilder.Entity<ReservationTool>().HasData(
                new ReservationTool
                {
                    Id = 1,
                    ReservationId = 1,
                    ToolId = 1,
                    EstimatedPrice = 135.00m, 
                    EstimatedDays = 3,
                    Notes = "Behöver för borrning i betong",
                   
                },
                new ReservationTool
                {
                    Id = 2,
                    ReservationId = 1,
                    ToolId = 6,
                    EstimatedPrice = 280.00m, 
                    EstimatedDays = 7,
                    Notes = "För exakt mätning av utrymme",
                   
                }
            );
            // Seed OrderDetails
            modelBuilder.Entity<OrderDeatils>().HasData(
                new OrderDeatils
                {
                    Id = 1,
                    Date2Hire = DateTime.UtcNow.AddDays(-5),
                    Date2Return = DateTime.UtcNow.AddDays(-2),
                    Status = "Returned",
                    TotalPrice = 105.00m, // 3 dagar * 35kr
                    LateFee = 0.00m,
                    CheckedOutAt = DateTime.UtcNow.AddDays(-5),
                    ReturnedAt = DateTime.UtcNow.AddDays(-2),
                    UserId = 3,
                    ToolId = 2, // Vinkelslip

                }
            );


        }

    }
}
