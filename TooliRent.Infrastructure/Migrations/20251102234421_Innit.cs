using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TooliRent.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Innit : Migration
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
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
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
                    { 4, "Skyddsutrustning för säkert arbete", "Säkerhetsutrustning" },
                    { 5, "Verktyg för trädgårdsarbete och utomhusbruk", "Trädgårdsverktyg" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "IsActive", "LastName", "PasswordHash", "Role" },
                values: new object[,]
                {
                    { 1, "Leon.Admin@example.com", "Leon", true, "Johansson", "Adminpower", "Admin" },
                    { 2, "DoeTheJohn@example.com", "John", true, "Doe", "password123", "Member" },
                    { 3, "StarTrekFan@example.com", "Petter", true, "Boström", "Maythe4", "Member" },
                    { 4, "emma.andersson@example.com", "Emma", true, "Andersson", "Emma2024!", "Member" },
                    { 5, "oscar.l@example.com", "Oscar", true, "Lindberg", "Oscar123", "Member" },
                    { 6, "sara.k@example.com", "Sara", false, "Karlsson", "SaraK2024", "Member" }
                });

            migrationBuilder.InsertData(
                table: "Tools",
                columns: new[] { "Id", "Brand", "CategoryId", "Description", "IsAvailable", "Model", "Name", "PricePerDay", "SerialNumber" },
                values: new object[,]
                {
                    { 1, "Bosch", 1, "Professionell slagborrmaskin för betong och murning", true, "GSB 13 RE", "Slagborrmaskin", 45.00m, "BOSCH-001" },
                    { 2, "Makita", 1, "125mm vinkelslip för kapning och slipning", true, "GA5030", "Vinkelslip", 35.00m, "MAKITA-002" },
                    { 3, "Festool", 1, "Precisionsticksåg med pendelfunktion", true, "PS 420 EBQ", "Sticksåg", 55.00m, "FESTOOL-003" },
                    { 4, "DeWalt", 1, "Kraftfull cirkelsåg med 1600W motor", true, "DWE575K", "Cirkelsåg", 50.00m, "DEWALT-004" },
                    { 5, "Bosch", 1, "Kompakt skruvdragare med 2 batterier", true, "GSR 18V-21", "Batteridriven skruvdragare", 40.00m, "BOSCH-005" },
                    { 6, "Stanley", 2, "Kluvspark 450g med glasfiberhandtag", true, "STHT0-51309", "Hammare", 15.00m, "STANLEY-006" },
                    { 7, "Wera", 2, "Set med 6 isolerade skruvmejslar", true, "Kraftform Plus", "Skruvmejselset", 20.00m, "WERA-007" },
                    { 8, "Bahco", 2, "Universalhandsåg med XT-tandning", true, "2600-19-XT", "Handsåg", 25.00m, "BAHCO-008" },
                    { 9, "Leica", 3, "Precision laserdistansmätare upp till 100m", true, "DISTO D2", "Lasermätare", 40.00m, "LEICA-009" },
                    { 10, "Stabila", 3, "Aluminium vattenpass 60cm med 3 libeller", true, "70-2", "Vattenpass", 25.00m, "STABILA-010" },
                    { 11, "Stanley", 3, "Extra starkt måttband med magnetisk krok", true, "FatMax", "Måttband 10m", 10.00m, "STANLEY-011" },
                    { 12, "3M", 4, "Ventilerad skyddshjälm med justerbart huvudband", true, "SecureFit X5000", "Skyddshjälm", 10.00m, "3M-012" },
                    { 13, "Uvex", 4, "Skyddsglasögon med anti-imma coating", false, "i-vo", "Skyddsglasögon", 8.00m, "UVEX-013" },
                    { 14, "3M", 4, "Professionellt hörselskydd med SNR 37dB", true, "Peltor X5A", "Hörselskydd", 12.00m, "3M-014" },
                    { 15, "Husqvarna", 5, "Bensindriven grästrimmer med 28cc motor", true, "129R", "Grästrimmer", 60.00m, "HUSQ-015" },
                    { 16, "Stihl", 5, "Bensindriven häcksax 60cm blad", true, "HS 45", "Häcksax", 55.00m, "STIHL-016" },
                    { 17, "Makita", 5, "Kraftfull 4-takts lövblåsare", true, "BHX2501", "Lövblåsare", 45.00m, "MAKITA-017" }
                });

            migrationBuilder.InsertData(
                table: "OrderDeatils",
                columns: new[] { "Id", "CheckedOutAt", "CreatedAt", "Date2Hire", "Date2Return", "LateFee", "ReservationId", "ReturnedAt", "Status", "ToolId", "TotalPrice", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 9, 1, 8, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 8, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.00m, null, new DateTime(2024, 9, 4, 16, 30, 0, 0, DateTimeKind.Unspecified), "Returned", 2, 105.00m, new DateTime(2024, 9, 4, 16, 30, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 2, new DateTime(2024, 9, 5, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.00m, null, new DateTime(2024, 9, 10, 15, 0, 0, 0, DateTimeKind.Unspecified), "Returned", 1, 270.00m, new DateTime(2024, 9, 10, 15, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 3, new DateTime(2024, 9, 8, 10, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 50.00m, null, new DateTime(2024, 9, 10, 11, 0, 0, 0, DateTimeKind.Unspecified), "Returned", 7, 40.00m, new DateTime(2024, 9, 10, 11, 0, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 4, new DateTime(2024, 9, 12, 8, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.00m, null, new DateTime(2024, 9, 15, 14, 0, 0, 0, DateTimeKind.Unspecified), "Returned", 10, 120.00m, new DateTime(2024, 9, 15, 14, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 5, new DateTime(2024, 10, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.00m, null, new DateTime(2024, 10, 3, 16, 0, 0, 0, DateTimeKind.Unspecified), "Returned", 3, 165.00m, new DateTime(2024, 10, 3, 16, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 6, new DateTime(2024, 10, 20, 8, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "CheckedOut", 9, 200.00m, new DateTime(2024, 10, 20, 8, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 7, new DateTime(2024, 10, 22, 9, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "CheckedOut", 4, 300.00m, new DateTime(2024, 10, 22, 9, 30, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 8, null, new DateTime(2024, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Pending", 15, 180.00m, new DateTime(2024, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 9, null, new DateTime(2024, 10, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Pending", 14, 90.00m, new DateTime(2024, 10, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 10, null, new DateTime(2024, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Cancelled", 16, 165.00m, new DateTime(2024, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 }
                });

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
