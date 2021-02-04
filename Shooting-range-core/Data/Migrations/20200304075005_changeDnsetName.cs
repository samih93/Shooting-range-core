using Microsoft.EntityFrameworkCore.Migrations;

namespace Shooting_range_core.Data.Migrations
{
    public partial class changeDnsetName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tournament_ShootingField_ShootingFieldId",
                table: "Tournament");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tournament",
                table: "Tournament");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShootingField",
                table: "ShootingField");

            migrationBuilder.RenameTable(
                name: "Tournament",
                newName: "Tournaments");

            migrationBuilder.RenameTable(
                name: "ShootingField",
                newName: "ShootingFields");

            migrationBuilder.RenameIndex(
                name: "IX_Tournament_ShootingFieldId",
                table: "Tournaments",
                newName: "IX_Tournaments_ShootingFieldId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tournaments",
                table: "Tournaments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShootingFields",
                table: "ShootingFields",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tournaments_ShootingFields_ShootingFieldId",
                table: "Tournaments",
                column: "ShootingFieldId",
                principalTable: "ShootingFields",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tournaments_ShootingFields_ShootingFieldId",
                table: "Tournaments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tournaments",
                table: "Tournaments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShootingFields",
                table: "ShootingFields");

            migrationBuilder.RenameTable(
                name: "Tournaments",
                newName: "Tournament");

            migrationBuilder.RenameTable(
                name: "ShootingFields",
                newName: "ShootingField");

            migrationBuilder.RenameIndex(
                name: "IX_Tournaments_ShootingFieldId",
                table: "Tournament",
                newName: "IX_Tournament_ShootingFieldId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tournament",
                table: "Tournament",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShootingField",
                table: "ShootingField",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tournament_ShootingField_ShootingFieldId",
                table: "Tournament",
                column: "ShootingFieldId",
                principalTable: "ShootingField",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
