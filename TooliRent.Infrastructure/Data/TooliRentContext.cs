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
        public DbSet<OrderDetails> OrderDeatils { get; set; }

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

            modelBuilder.Entity<OrderDetails>()
                .Property(o => o.TotalPrice)
                .HasPrecision(10, 2);

            modelBuilder.Entity<OrderDetails>()
                .Property(o => o.LateFee)
                .HasPrecision(10, 2);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // ========== SEED USERS ==========
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    FirstName = "Leon",
                    LastName = "Johansson",
                    Email = "Leon.Admin@example.com",
                    PasswordHash = "Adminpower",
                    Role = "Admin",
                    IsActive = true
                },
                new User
                {
                    Id = 2,
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "DoeTheJohn@example.com",
                    PasswordHash = "password123",
                    Role = "Member",
                    IsActive = true
                },
                new User
                {
                    Id = 3,
                    FirstName = "Petter",
                    LastName = "Boström",
                    Email = "StarTrekFan@example.com",
                    PasswordHash = "Maythe4",
                    Role = "Member",
                    IsActive = true
                },
                new User
                {
                    Id = 4,
                    FirstName = "Emma",
                    LastName = "Andersson",
                    Email = "emma.andersson@example.com",
                    PasswordHash = "Emma2024!",
                    Role = "Member",
                    IsActive = true
                },
                new User
                {
                    Id = 5,
                    FirstName = "Oscar",
                    LastName = "Lindberg",
                    Email = "oscar.l@example.com",
                    PasswordHash = "Oscar123",
                    Role = "Member",
                    IsActive = true
                },
                new User
                {
                    Id = 6,
                    FirstName = "Sara",
                    LastName = "Karlsson",
                    Email = "sara.k@example.com",
                    PasswordHash = "SaraK2024",
                    Role = "Member",
                    IsActive = false  // Not active user
                }
            );

            // ========== SEED CATEGORIES ==========
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
                },
                new Category
                {
                    Id = 5,
                    Name = "Trädgårdsverktyg",
                    Description = "Verktyg för trädgårdsarbete och utomhusbruk"
                }
            );

            // ========== SEED TOOLS ==========
            modelBuilder.Entity<Tool>().HasData(
                // Electric Tools
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
                new Tool
                {
                    Id = 4,
                    Name = "Cirkelsåg",
                    Brand = "DeWalt",
                    Model = "DWE575K",
                    Description = "Kraftfull cirkelsåg med 1600W motor",
                    PricePerDay = 50.00m,
                    IsAvailable = true,
                    SerialNumber = "DEWALT-004",
                    CategoryId = 1
                },
                new Tool
                {
                    Id = 5,
                    Name = "Batteridriven skruvdragare",
                    Brand = "Bosch",
                    Model = "GSR 18V-21",
                    Description = "Kompakt skruvdragare med 2 batterier",
                    PricePerDay = 40.00m,
                    IsAvailable = true,
                    SerialNumber = "BOSCH-005",
                    CategoryId = 1
                },

                
                new Tool
                {
                    Id = 6,
                    Name = "Hammare",
                    Brand = "Stanley",
                    Model = "STHT0-51309",
                    Description = "Kluvspark 450g med glasfiberhandtag",
                    PricePerDay = 15.00m,
                    IsAvailable = true,
                    SerialNumber = "STANLEY-006",
                    CategoryId = 2
                },
                new Tool
                {
                    Id = 7,
                    Name = "Skruvmejselset",
                    Brand = "Wera",
                    Model = "Kraftform Plus",
                    Description = "Set med 6 isolerade skruvmejslar",
                    PricePerDay = 20.00m,
                    IsAvailable = true,
                    SerialNumber = "WERA-007",
                    CategoryId = 2
                },
                new Tool
                {
                    Id = 8,
                    Name = "Handsåg",
                    Brand = "Bahco",
                    Model = "2600-19-XT",
                    Description = "Universalhandsåg med XT-tandning",
                    PricePerDay = 25.00m,
                    IsAvailable = true,
                    SerialNumber = "BAHCO-008",
                    CategoryId = 2
                },

                // Mätverktyg
                new Tool
                {
                    Id = 9,
                    Name = "Lasermätare",
                    Brand = "Leica",
                    Model = "DISTO D2",
                    Description = "Precision laserdistansmätare upp till 100m",
                    PricePerDay = 40.00m,
                    IsAvailable = true,
                    SerialNumber = "LEICA-009",
                    CategoryId = 3
                },
                new Tool
                {
                    Id = 10,
                    Name = "Vattenpass",
                    Brand = "Stabila",
                    Model = "70-2",
                    Description = "Aluminium vattenpass 60cm med 3 libeller",
                    PricePerDay = 25.00m,
                    IsAvailable = true,
                    SerialNumber = "STABILA-010",
                    CategoryId = 3
                },
                new Tool
                {
                    Id = 11,
                    Name = "Måttband 10m",
                    Brand = "Stanley",
                    Model = "FatMax",
                    Description = "Extra starkt måttband med magnetisk krok",
                    PricePerDay = 10.00m,
                    IsAvailable = true,
                    SerialNumber = "STANLEY-011",
                    CategoryId = 3
                },

                // Säkerhetsutrustning
                new Tool
                {
                    Id = 12,
                    Name = "Skyddshjälm",
                    Brand = "3M",
                    Model = "SecureFit X5000",
                    Description = "Ventilerad skyddshjälm med justerbart huvudband",
                    PricePerDay = 10.00m,
                    IsAvailable = true,
                    SerialNumber = "3M-012",
                    CategoryId = 4
                },
                new Tool
                {
                    Id = 13,
                    Name = "Skyddsglasögon",
                    Brand = "Uvex",
                    Model = "i-vo",
                    Description = "Skyddsglasögon med anti-imma coating",
                    PricePerDay = 8.00m,
                    IsAvailable = false,  // Under underhåll
                    SerialNumber = "UVEX-013",
                    CategoryId = 4
                },
                new Tool
                {
                    Id = 14,
                    Name = "Hörselskydd",
                    Brand = "3M",
                    Model = "Peltor X5A",
                    Description = "Professionellt hörselskydd med SNR 37dB",
                    PricePerDay = 12.00m,
                    IsAvailable = true,
                    SerialNumber = "3M-014",
                    CategoryId = 4
                },

                // Trädgårdsverktyg
                new Tool
                {
                    Id = 15,
                    Name = "Grästrimmer",
                    Brand = "Husqvarna",
                    Model = "129R",
                    Description = "Bensindriven grästrimmer med 28cc motor",
                    PricePerDay = 60.00m,
                    IsAvailable = true,
                    SerialNumber = "HUSQ-015",
                    CategoryId = 5
                },
                new Tool
                {
                    Id = 16,
                    Name = "Häcksax",
                    Brand = "Stihl",
                    Model = "HS 45",
                    Description = "Bensindriven häcksax 60cm blad",
                    PricePerDay = 55.00m,
                    IsAvailable = true,
                    SerialNumber = "STIHL-016",
                    CategoryId = 5
                },
                new Tool
                {
                    Id = 17,
                    Name = "Lövblåsare",
                    Brand = "Makita",
                    Model = "BHX2501",
                    Description = "Kraftfull 4-takts lövblåsare",
                    PricePerDay = 45.00m,
                    IsAvailable = true,
                    SerialNumber = "MAKITA-017",
                    CategoryId = 5
                }
            );

            // ========== SEED ORDER DETAILS ==========
            modelBuilder.Entity<OrderDetails>().HasData(
                // Returned orders (avslutade)
                new OrderDetails
                {
                    Id = 1,
                    Date2Hire = new DateTime(2024, 9, 1),
                    Date2Return = new DateTime(2024, 9, 4),
                    Status = "Returned",
                    TotalPrice = 105.00m,
                    LateFee = 0.00m,
                    CheckedOutAt = new DateTime(2024, 9, 1, 8, 0, 0),
                    ReturnedAt = new DateTime(2024, 9, 4, 16, 30, 0),
                    CreatedAt = new DateTime(2024, 8, 28),
                    UpdatedAt = new DateTime(2024, 9, 4, 16, 30, 0),
                    UserId = 3,
                    ToolId = 2,
                    ReservationId = null
                },
                new OrderDetails
                {
                    Id = 2,
                    Date2Hire = new DateTime(2024, 9, 5),
                    Date2Return = new DateTime(2024, 9, 10),
                    Status = "Returned",
                    TotalPrice = 270.00m,  // 6 days * 45kr
                    LateFee = 0.00m,
                    CheckedOutAt = new DateTime(2024, 9, 5, 9, 0, 0),
                    ReturnedAt = new DateTime(2024, 9, 10, 15, 0, 0),
                    CreatedAt = new DateTime(2024, 9, 1),
                    UpdatedAt = new DateTime(2024, 9, 10, 15, 0, 0),
                    UserId = 2,
                    ToolId = 1,
                    ReservationId = null
                },
                new OrderDetails
                {
                    Id = 3,
                    Date2Hire = new DateTime(2024, 9, 8),
                    Date2Return = new DateTime(2024, 9, 9),
                    Status = "Returned",
                    TotalPrice = 40.00m,  // 2 days * 20kr
                    LateFee = 50.00m,  // 1 day late
                    CheckedOutAt = new DateTime(2024, 9, 8, 10, 0, 0),
                    ReturnedAt = new DateTime(2024, 9, 10, 11, 0, 0),  // 1 dag för sent
                    CreatedAt = new DateTime(2024, 9, 5),
                    UpdatedAt = new DateTime(2024, 9, 10, 11, 0, 0),
                    UserId = 4,
                    ToolId = 7,
                    ReservationId = null
                },
                new OrderDetails
                {
                    Id = 4,
                    Date2Hire = new DateTime(2024, 9, 12),
                    Date2Return = new DateTime(2024, 9, 15),
                    Status = "Returned",
                    TotalPrice = 120.00m,  // 4 days * 30kr
                    LateFee = 0.00m,
                    CheckedOutAt = new DateTime(2024, 9, 12, 8, 30, 0),
                    ReturnedAt = new DateTime(2024, 9, 15, 14, 0, 0),
                    CreatedAt = new DateTime(2024, 9, 8),
                    UpdatedAt = new DateTime(2024, 9, 15, 14, 0, 0),
                    UserId = 5,
                    ToolId = 10,
                    ReservationId = null
                },
                new OrderDetails
                {
                    Id = 5,
                    Date2Hire = new DateTime(2024, 10, 1),
                    Date2Return = new DateTime(2024, 10, 3),
                    Status = "Returned",
                    TotalPrice = 165.00m,  // 3 days * 55kr
                    LateFee = 0.00m,
                    CheckedOutAt = new DateTime(2024, 10, 1, 9, 0, 0),
                    ReturnedAt = new DateTime(2024, 10, 3, 16, 0, 0),
                    CreatedAt = new DateTime(2024, 9, 28),
                    UpdatedAt = new DateTime(2024, 10, 3, 16, 0, 0),
                    UserId = 2,
                    ToolId = 3,
                    ReservationId = null
                },

                // CheckedOut orders (utlånade just nu)
                new OrderDetails
                {
                    Id = 6,
                    Date2Hire = new DateTime(2024, 10, 20),
                    Date2Return = new DateTime(2024, 10, 25),
                    Status = "CheckedOut",
                    TotalPrice = 200.00m,  // 5 days * 40kr
                    LateFee = null,
                    CheckedOutAt = new DateTime(2024, 10, 20, 8, 0, 0),
                    ReturnedAt = null,
                    CreatedAt = new DateTime(2024, 10, 15),
                    UpdatedAt = new DateTime(2024, 10, 20, 8, 0, 0),
                    UserId = 3,
                    ToolId = 9,
                    ReservationId = null
                },
                new OrderDetails
                {
                    Id = 7,
                    Date2Hire = new DateTime(2024, 10, 22),
                    Date2Return = new DateTime(2024, 10, 27),
                    Status = "CheckedOut",
                    TotalPrice = 300.00m,  // 6 days * 50kr
                    LateFee = null,
                    CheckedOutAt = new DateTime(2024, 10, 22, 9, 30, 0),
                    ReturnedAt = null,
                    CreatedAt = new DateTime(2024, 10, 18),
                    UpdatedAt = new DateTime(2024, 10, 22, 9, 30, 0),
                    UserId = 4,
                    ToolId = 4,
                    ReservationId = null
                },

                // Pending orders (kommande)
                new OrderDetails
                {
                    Id = 8,
                    Date2Hire = new DateTime(2024, 11, 5),
                    Date2Return = new DateTime(2024, 11, 8),
                    Status = "Pending",
                    TotalPrice = 180.00m,  // 4 days * 45kr
                    LateFee = null,
                    CheckedOutAt = null,
                    ReturnedAt = null,
                    CreatedAt = new DateTime(2024, 10, 25),
                    UpdatedAt = new DateTime(2024, 10, 25),
                    UserId = 2,
                    ToolId = 15,
                    ReservationId = null
                },
                new OrderDetails
                {
                    Id = 9,
                    Date2Hire = new DateTime(2024, 11, 10),
                    Date2Return = new DateTime(2024, 11, 12),
                    Status = "Pending",
                    TotalPrice = 90.00m,  // 3 days * 30kr
                    LateFee = null,
                    CheckedOutAt = null,
                    ReturnedAt = null,
                    CreatedAt = new DateTime(2024, 10, 28),
                    UpdatedAt = new DateTime(2024, 10, 28),
                    UserId = 5,
                    ToolId = 14,
                    ReservationId = null
                },

                // Cancelled order
                new OrderDetails
                {
                    Id = 10,
                    Date2Hire = new DateTime(2024, 10, 15),
                    Date2Return = new DateTime(2024, 10, 18),
                    Status = "Cancelled",
                    TotalPrice = 165.00m,  // 3 days * 55kr
                    LateFee = null,
                    CheckedOutAt = null,
                    ReturnedAt = null,
                    CreatedAt = new DateTime(2024, 10, 10),
                    UpdatedAt = new DateTime(2024, 10, 12),
                    UserId = 3,
                    ToolId = 16,
                    ReservationId = null
                }
            );
        }
    }
}

