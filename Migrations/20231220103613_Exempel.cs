using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Databaser_Labb3_V2.Migrations
{
    /// <inheritdoc />
    public partial class Exempel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AvdelningId",
                table: "Betyg",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FkAvdelningId",
                table: "Betyg",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Betyg_AvdelningId",
                table: "Betyg",
                column: "AvdelningId");

            migrationBuilder.AddForeignKey(
                name: "FK_Betyg_Avdelning_AvdelningId",
                table: "Betyg",
                column: "AvdelningId",
                principalTable: "Avdelning",
                principalColumn: "AvdelningId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Betyg_Avdelning_AvdelningId",
                table: "Betyg");

            migrationBuilder.DropIndex(
                name: "IX_Betyg_AvdelningId",
                table: "Betyg");

            migrationBuilder.DropColumn(
                name: "AvdelningId",
                table: "Betyg");

            migrationBuilder.DropColumn(
                name: "FkAvdelningId",
                table: "Betyg");
        }
    }
}
