using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MainServiceAPI.Migrations
{
    public partial class AgainChangeDoctorActiveNowType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsActiveNow",
                table: "Doctors",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "IsActiveNow",
                table: "Doctors",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");
        }
    }
}
