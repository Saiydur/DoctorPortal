using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MainServiceAPI.Migrations
{
    public partial class CategoryChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consultations_Categories_CategoryId",
                table: "Consultations");

            migrationBuilder.AddForeignKey(
                name: "FK_Consultations_Categories_CategoryId",
                table: "Consultations",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consultations_Categories_CategoryId",
                table: "Consultations");

            migrationBuilder.AddForeignKey(
                name: "FK_Consultations_Categories_CategoryId",
                table: "Consultations",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
