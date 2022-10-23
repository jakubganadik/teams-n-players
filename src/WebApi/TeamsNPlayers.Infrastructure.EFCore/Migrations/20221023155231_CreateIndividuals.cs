using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamsNPlayers.Infrastructure.EFCore.Migrations
{
    public partial class CreateIndividuals : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "individuals",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    first_name = table.Column<string>(type: "TEXT", nullable: false),
                    last_name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_individuals", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "individuals",
                columns: new[] { "id", "first_name", "last_name" },
                values: new object[] { new Guid("4e0fc4d0-7cfb-4fc9-b5b5-51789e9787cb"), "Edgar", "Allan Poe" });

            migrationBuilder.InsertData(
                table: "individuals",
                columns: new[] { "id", "first_name", "last_name" },
                values: new object[] { new Guid("899340ea-841c-473f-a0d6-093d2bbe1474"), "Jane", "Doe" });

            migrationBuilder.InsertData(
                table: "individuals",
                columns: new[] { "id", "first_name", "last_name" },
                values: new object[] { new Guid("ed774e4c-5fb6-48b1-b8fa-61793cbbdc50"), "John", "Smith" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "individuals");
        }
    }
}
