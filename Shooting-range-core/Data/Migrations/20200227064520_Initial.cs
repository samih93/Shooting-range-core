using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shooting_range_core.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ShootingField",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShootingName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShootingField", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tournament",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TournamentName = table.Column<string>(nullable: true),
                    TournamentDate = table.Column<DateTime>(nullable: false),
                    FromTime = table.Column<string>(nullable: true),
                    ToTime = table.Column<string>(nullable: true),
                    WeaponKind = table.Column<string>(nullable: true),
                    ShootingFieldId = table.Column<int>(nullable: true),
                    ColorCode = table.Column<string>(nullable: true),
                    Notes = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tournament", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShootingField");

            migrationBuilder.DropTable(
                name: "Tournament");
        }
    }
}
