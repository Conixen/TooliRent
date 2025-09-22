using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TooliRent.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tools",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Model = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    PricePerDay = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tools", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tools_Categorys_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categorys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date2Hire = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Date2Return = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CanceledAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CanceledReason = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderDeatils",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date2Hire = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Date2Return = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    LateFee = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    CheckedOutAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReturnedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ToolId = table.Column<int>(type: "int", nullable: false),
                    ReservationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDeatils", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDeatils_Reservations_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "Reservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_OrderDeatils_Tools_ToolId",
                        column: x => x.ToolId,
                        principalTable: "Tools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderDeatils_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReservationTools",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ToolId = table.Column<int>(type: "int", nullable: false),
                    ReservationId = table.Column<int>(type: "int", nullable: false),
                    EstimatedPrice = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    EstimatedDays = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationTools", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReservationTools_Reservations_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "Reservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReservationTools_Tools_ToolId",
                        column: x => x.ToolId,
                        principalTable: "Tools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categorys",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Elektriska verktyg för byggarbete och hantverk", "Elverktyg" },
                    { 2, "Traditionella handverktyg för precision och detaljarbete", "Handverktyg" },
                    { 3, "Verktyg för mätning och märkning", "Mätverktyg" },
                    { 4, "Skyddsutrustning för säkert arbete", "Säkerhetsutrustning" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "PasswordHash", "Role" },
                values: new object[,]
                {
                    { 1, "Leon.Johanssonsens@example.com", "Leon", "Johansson", "$2a$11$9Ew4j1GhTpXqMNXlBJ6zU.2Gq8FYzrLX5K9JzE3X7wMdA8V4qZrOm", "Admin" },
                    { 2, "DoeTheJohn@example.com", "John", "Doe", "$2a$11$9Ew4j1GhTpXqMNXlBJ6zU.2Gq8FYzrLX5K9JzE3X7wMdA8V4qZrOm", "Member" },
                    { 3, "StarTrekFan@example.com", "Petter", "Boström", "$2a$11$9Ew4j1GhTpXqMNXlBJ6zU.2Gq8FYzrLX5K9JzE3X7wMdA8V4qZrOm", "Member" }
                });

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "Id", "CanceledAt", "CanceledReason", "Date2Hire", "Date2Return", "Status", "UserId" },
                values: new object[] { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2025, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Active", 2 });

            migrationBuilder.InsertData(
                table: "Tools",
                columns: new[] { "Id", "Brand", "CategoryId", "Description", "IsAvailable", "Model", "Name", "PricePerDay", "Status" },
                values: new object[,]
                {
                    { 1, "Bosch", 1, "Professionell slagborrmaskin för betong och murning", true, "GSB 13 RE", "Slagborrmaskin", 45.00m, "Available" },
                    { 2, "Makita", 1, "125mm vinkelslip för kapning och slipning", true, "GA5030", "Vinkelslip", 35.00m, "Available" },
                    { 3, "Festool", 1, "Precisionsticksåg med pendelfunktion", true, "PS 420 EBQ", "Sticksåg", 55.00m, "Available" },
                    { 4, "Stanley", 2, "Kluvspark 450g med glasfiberhandtag", true, "STHT0-51309", "Hammare", 15.00m, "Available" },
                    { 5, "Wera", 2, "Set med 6 isolerade skruvmejslar", true, "Kraftform Plus", "Skruvmejselset", 20.00m, "Available" },
                    { 6, "Leica", 3, "Precision laserdistansmätare upp till 100m", true, "DISTO D2", "Lasermätare", 40.00m, "Available" },
                    { 7, "Stabila", 3, "Aluminium vattenpass 60cm med 3 libeller", true, "70-2", "Vattenpass", 25.00m, "Available" },
                    { 8, "3M", 4, "Ventilerad skyddshjälm med justerbart huvudband", true, "SecureFit X5000", "Skyddshjälm", 10.00m, "Available" },
                    { 9, "Uvex", 4, "Skyddsglasögon med anti-imma coating", false, "i-vo", "Skyddsglasögon", 8.00m, "Maintenance" }
                });

            migrationBuilder.InsertData(
                table: "OrderDeatils",
                columns: new[] { "Id", "CheckedOutAt", "Date2Hire", "Date2Return", "LateFee", "ReservationId", "ReturnedAt", "Status", "ToolId", "TotalPrice", "UserId" },
                values: new object[] { 1, new DateTime(2025, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.00m, null, new DateTime(2025, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Returned", 2, 105.00m, 3 });

            migrationBuilder.InsertData(
                table: "ReservationTools",
                columns: new[] { "Id", "EstimatedDays", "EstimatedPrice", "Notes", "ReservationId", "ToolId" },
                values: new object[,]
                {
                    { 1, 3, 135.00m, "Behöver för borrning i betong", 1, 1 },
                    { 2, 7, 280.00m, "För exakt mätning av utrymme", 1, 6 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderDeatils_ReservationId",
                table: "OrderDeatils",
                column: "ReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDeatils_ToolId",
                table: "OrderDeatils",
                column: "ToolId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDeatils_UserId",
                table: "OrderDeatils",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_UserId",
                table: "Reservations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationTools_ReservationId_ToolId",
                table: "ReservationTools",
                columns: new[] { "ReservationId", "ToolId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReservationTools_ToolId",
                table: "ReservationTools",
                column: "ToolId");

            migrationBuilder.CreateIndex(
                name: "IX_Tools_CategoryId",
                table: "Tools",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderDeatils");

            migrationBuilder.DropTable(
                name: "ReservationTools");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Tools");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Categorys");
        }
    }
}
