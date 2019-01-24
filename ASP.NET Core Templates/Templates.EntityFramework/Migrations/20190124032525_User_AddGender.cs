using Microsoft.EntityFrameworkCore.Migrations;

namespace Templates.EntityFrameworkCore.Migrations
{
    public partial class User_AddGender : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Gender",
                table: "users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "users");
        }
    }
}
