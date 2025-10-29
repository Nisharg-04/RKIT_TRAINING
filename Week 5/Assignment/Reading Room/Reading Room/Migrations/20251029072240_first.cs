using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Reading_Room.Migrations
{
    /// <inheritdoc />
    public partial class first : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Capacity = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoomId = table.Column<int>(type: "INTEGER", nullable: false),
                    PatronName = table.Column<string>(type: "TEXT", nullable: false),
                    Start = table.Column<DateTime>(type: "TEXT", nullable: false),
                    End = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "Capacity", "Name" },
                values: new object[,]
                {
                    { 1, 6, "Room A" },
                    { 2, 10, "Room B" },
                    { 3, 4, "Room C" }
                });

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "Id", "End", "PatronName", "RoomId", "Start", "Status" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 10, 27, 9, 22, 38, 362, DateTimeKind.Utc).AddTicks(187), "Alice", 1, new DateTime(2025, 10, 27, 7, 22, 38, 361, DateTimeKind.Utc).AddTicks(9799), 1 },
                    { 2, new DateTime(2025, 10, 28, 10, 22, 38, 362, DateTimeKind.Utc).AddTicks(532), "Bob", 1, new DateTime(2025, 10, 28, 7, 22, 38, 362, DateTimeKind.Utc).AddTicks(530), 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_RoomId_Start_End",
                table: "Reservations",
                columns: new[] { "RoomId", "Start", "End" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Rooms");
        }
    }
}
