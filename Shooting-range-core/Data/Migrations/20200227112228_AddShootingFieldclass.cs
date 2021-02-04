using Microsoft.EntityFrameworkCore.Migrations;

namespace Shooting_range_core.Data.Migrations
{
    public partial class AddShootingFieldclass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Tournament_ShootingFieldId",
                table: "Tournament",
                column: "ShootingFieldId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tournament_ShootingField_ShootingFieldId",
                table: "Tournament",
                column: "ShootingFieldId",
                principalTable: "ShootingField",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tournament_ShootingField_ShootingFieldId",
                table: "Tournament");

            migrationBuilder.DropIndex(
                name: "IX_Tournament_ShootingFieldId",
                table: "Tournament");
        }
    }
}
