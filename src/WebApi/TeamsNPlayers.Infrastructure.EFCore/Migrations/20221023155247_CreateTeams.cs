using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamsNPlayers.Infrastructure.EFCore.Migrations
{
    public partial class CreateTeams : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "teams",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_teams", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "teams",
                columns: new[] { "id", "name" },
                values: new object[] { new Guid("5737a2e9-02d8-4a9e-87d1-6808954d7de3"), "AC Bar" });

            migrationBuilder.InsertData(
                table: "teams",
                columns: new[] { "id", "name" },
                values: new object[] { new Guid("79d1705c-4864-4a29-a864-2ec582bb6342"), "FC Foo" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "teams");
        }
    }
}
