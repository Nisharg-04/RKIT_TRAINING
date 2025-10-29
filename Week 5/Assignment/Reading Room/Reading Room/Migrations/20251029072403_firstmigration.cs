using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Reading_Room.Migrations
{
    /// <inheritdoc />
    public partial class firstmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 2);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "Id", "End", "PatronName", "RoomId", "Start", "Status" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 10, 27, 9, 22, 38, 362, DateTimeKind.Utc).AddTicks(187), "Alice", 1, new DateTime(2025, 10, 27, 7, 22, 38, 361, DateTimeKind.Utc).AddTicks(9799), 1 },
                    { 2, new DateTime(2025, 10, 28, 10, 22, 38, 362, DateTimeKind.Utc).AddTicks(532), "Bob", 1, new DateTime(2025, 10, 28, 7, 22, 38, 362, DateTimeKind.Utc).AddTicks(530), 1 }
                });
        }
    }
}
