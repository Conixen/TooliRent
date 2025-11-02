using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TooliRent.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
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
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PricePerDay = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                name: "OrderDeatils",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date2Hire = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Date2Return = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    LateFee = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    CheckedOutAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReturnedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ToolId = table.Column<int>(type: "int", nullable: false),
                    ReservationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDeatils", x => x.Id);
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
                    { 1, "Leon.Johanssonsens@example.com", "Leon", "Johansson", "Adminpower", "Admin" },
                    { 2, "DoeTheJohn@example.com", "John", "Doe", "password123", "Member" },
                    { 3, "StarTrekFan@example.com", "Petter", "Boström", "Maythe4", "Member" }
                });

            migrationBuilder.InsertData(
                table: "Tools",
                columns: new[] { "Id", "Brand", "CategoryId", "Description", "IsAvailable", "Model", "Name", "PricePerDay", "SerialNumber" },
                values: new object[,]
                {
                    { 1, "Bosch", 1, "Professionell slagborrmaskin för betong och murning", true, "GSB 13 RE", "Slagborrmaskin", 45.00m, "BOSCH-001" },
                    { 2, "Makita", 1, "125mm vinkelslip för kapning och slipning", true, "GA5030", "Vinkelslip", 35.00m, "MAKITA-002" },
                    { 3, "Festool", 1, "Precisionsticksåg med pendelfunktion", true, "PS 420 EBQ", "Sticksåg", 55.00m, "FESTOOL-003" },
                    { 4, "Stanley", 2, "Kluvspark 450g med glasfiberhandtag", true, "STHT0-51309", "Hammare", 15.00m, "STANLEY-004" },
                    { 5, "Wera", 2, "Set med 6 isolerade skruvmejslar", true, "Kraftform Plus", "Skruvmejselset", 20.00m, "WERA-005" },
                    { 6, "Leica", 3, "Precision laserdistansmätare upp till 100m", true, "DISTO D2", "Lasermätare", 40.00m, "LEICA-006" },
                    { 7, "Stabila", 3, "Aluminium vattenpass 60cm med 3 libeller", true, "70-2", "Vattenpass", 25.00m, "STABILA-007" },
                    { 8, "3M", 4, "Ventilerad skyddshjälm med justerbart huvudband", true, "SecureFit X5000", "Skyddshjälm", 10.00m, "3M-008" },
                    { 9, "Uvex", 4, "Skyddsglasögon med anti-imma coating", false, "i-vo", "Skyddsglasögon", 8.00m, "UVEX-009" }
                });

            migrationBuilder.InsertData(
                table: "OrderDeatils",
                columns: new[] { "Id", "CheckedOutAt", "CreatedAt", "Date2Hire", "Date2Return", "LateFee", "ReservationId", "ReturnedAt", "Status", "ToolId", "TotalPrice", "UpdatedAt", "UserId" },
                values: new object[] { 1, new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.00m, null, new DateTime(2024, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Returned", 2, 105.00m, new DateTime(2024, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 });

            migrationBuilder.CreateIndex(
                name: "IX_OrderDeatils_ToolId",
                table: "OrderDeatils",
                column: "ToolId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDeatils_UserId",
                table: "OrderDeatils",
                column: "UserId");

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
                name: "Tools");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Categorys");
        }
    }
}
