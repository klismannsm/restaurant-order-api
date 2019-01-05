using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace RestaurantOrder.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dishes",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    TimeOfDay = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dishes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Input = table.Column<string>(nullable: true),
                    Output = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Dishes",
                columns: new[] { "Id", "CreatedAt", "Name", "TimeOfDay", "Type" },
                values: new object[,]
                {
                    { 1L, new DateTime(2019, 1, 5, 15, 42, 36, 864, DateTimeKind.Local).AddTicks(7600), "eggs", "morning", 1 },
                    { 2L, new DateTime(2019, 1, 5, 15, 42, 36, 874, DateTimeKind.Local).AddTicks(770), "toast", "morning", 2 },
                    { 3L, new DateTime(2019, 1, 5, 15, 42, 36, 874, DateTimeKind.Local).AddTicks(790), "coffee", "morning", 3 },
                    { 4L, new DateTime(2019, 1, 5, 15, 42, 36, 874, DateTimeKind.Local).AddTicks(800), "steak", "night", 1 },
                    { 5L, new DateTime(2019, 1, 5, 15, 42, 36, 874, DateTimeKind.Local).AddTicks(800), "potato", "night", 2 },
                    { 6L, new DateTime(2019, 1, 5, 15, 42, 36, 874, DateTimeKind.Local).AddTicks(800), "wine", "night", 3 },
                    { 7L, new DateTime(2019, 1, 5, 15, 42, 36, 874, DateTimeKind.Local).AddTicks(810), "cake", "night", 4 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dishes");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
