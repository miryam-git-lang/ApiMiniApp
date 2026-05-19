using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ApiMiniApp.Migrations
{
    /// <inheritdoc />
    public partial class mig_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Organizers",
                columns: new[] { "Id", "CreatedAt", "Email", "IsDeleted", "LogoUrl", "Name", "Phone", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 5, 19, 16, 0, 0, 0, DateTimeKind.Utc), "contact@techconf.az", false, "/uploads/logos/techconf.png", "TechConf Inc.", "+994501234567", null },
                    { 2, new DateTime(2026, 5, 19, 16, 0, 0, 0, DateTimeKind.Utc), "info@artspace.az", false, "/uploads/logos/artspace.png", "ArtSpace Baku", "+994552345678", null },
                    { 3, new DateTime(2026, 5, 19, 16, 0, 0, 0, DateTimeKind.Utc), "hello@sportlife.az", false, "/uploads/logos/sportlife.png", "SportLife Agency", "+994703456789", null },
                    { 4, new DateTime(2026, 5, 19, 16, 0, 0, 0, DateTimeKind.Utc), "press@musicfest.az", false, "/uploads/logos/musicfest.png", "MusicFest Org", null, null },
                    { 5, new DateTime(2026, 5, 19, 16, 0, 0, 0, DateTimeKind.Utc), "edu@eduworld.az", false, null, "EduWorld Azerbaijan", "+994514567890", null }
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "BannerImageUrl", "CreatedAt", "Date", "Description", "IsDeleted", "Location", "OrganizerId", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "/uploads/banners/techsummit.jpg", new DateTime(2026, 5, 19, 16, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 9, 15, 10, 0, 0, 0, DateTimeKind.Unspecified), "Крупнейшая технологическая конференция года с участием мировых спикеров.", false, "Baku Convention Center, Баку", 1, "Tech Summit 2025", null },
                    { 2, "/uploads/banners/artfall.jpg", new DateTime(2026, 5, 19, 16, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 10, 5, 12, 0, 0, 0, DateTimeKind.Unspecified), "Выставка работ молодых художников Азербайджана.", false, "ArtSpace Gallery, ул. Низами 42, Баку", 2, "Современное искусство: Осень", null },
                    { 3, null, new DateTime(2026, 5, 19, 16, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 11, 1, 8, 0, 0, 0, DateTimeKind.Unspecified), "Ежегодный городской марафон по центру Баку.", false, "Площадь Азадлыг, Баку", 3, "Бакинский марафон 2025", null },
                    { 4, "/uploads/banners/jazz.jpg", new DateTime(2026, 5, 19, 16, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 8, 20, 19, 30, 0, 0, DateTimeKind.Unspecified), "Вечер живой джазовой музыки под открытым небом.", false, "Приморский бульвар, Баку", 4, "Jazz Under The Stars", null },
                    { 5, null, new DateTime(2026, 5, 19, 16, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 9, 25, 9, 0, 0, 0, DateTimeKind.Unspecified), "Интенсивный трёхдневный курс по Python для начинающих и продолжающих.", false, "EduWorld Campus, пр. Матбуат 102", 5, "Python Bootcamp", null },
                    { 6, "/uploads/banners/pitch.jpg", new DateTime(2026, 5, 19, 16, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 10, 18, 17, 0, 0, 0, DateTimeKind.Unspecified), "Вечер презентаций стартапов перед венчурными инвесторами.", false, "Baku Tech Hub, ул. Рашида Бейбутова 10", 1, "Startup Pitch Night", null }
                });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "CreatedAt", "EventId", "IsDeleted", "Price", "QuantityAvailable", "Type", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 5, 19, 16, 0, 0, 0, DateTimeKind.Utc), 1, false, 299.99m, 50, "VIP", null },
                    { 2, new DateTime(2026, 5, 19, 16, 0, 0, 0, DateTimeKind.Utc), 1, false, 99.99m, 300, "Regular", null },
                    { 3, new DateTime(2026, 5, 19, 16, 0, 0, 0, DateTimeKind.Utc), 1, false, 49.99m, 100, "Student", null },
                    { 4, new DateTime(2026, 5, 19, 16, 0, 0, 0, DateTimeKind.Utc), 2, false, 15.00m, 200, "Regular", null },
                    { 5, new DateTime(2026, 5, 19, 16, 0, 0, 0, DateTimeKind.Utc), 2, false, 35.00m, 30, "VIP", null },
                    { 6, new DateTime(2026, 5, 19, 16, 0, 0, 0, DateTimeKind.Utc), 3, false, 25.00m, 500, "Participant", null },
                    { 7, new DateTime(2026, 5, 19, 16, 0, 0, 0, DateTimeKind.Utc), 3, false, 75.00m, 20, "VIP", null },
                    { 8, new DateTime(2026, 5, 19, 16, 0, 0, 0, DateTimeKind.Utc), 4, false, 20.00m, 150, "Regular", null },
                    { 9, new DateTime(2026, 5, 19, 16, 0, 0, 0, DateTimeKind.Utc), 4, false, 60.00m, 40, "VIP", null },
                    { 10, new DateTime(2026, 5, 19, 16, 0, 0, 0, DateTimeKind.Utc), 4, false, 200.00m, 10, "Table", null },
                    { 11, new DateTime(2026, 5, 19, 16, 0, 0, 0, DateTimeKind.Utc), 5, false, 120.00m, 80, "Regular", null },
                    { 12, new DateTime(2026, 5, 19, 16, 0, 0, 0, DateTimeKind.Utc), 5, false, 199.99m, 15, "VIP", null },
                    { 13, new DateTime(2026, 5, 19, 16, 0, 0, 0, DateTimeKind.Utc), 6, false, 0.00m, 100, "General", null },
                    { 14, new DateTime(2026, 5, 19, 16, 0, 0, 0, DateTimeKind.Utc), 6, false, 50.00m, 25, "Investor", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Organizers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Organizers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Organizers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Organizers",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Organizers",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
