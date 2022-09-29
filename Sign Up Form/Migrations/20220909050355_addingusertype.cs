using Microsoft.EntityFrameworkCore.Migrations;


#nullable disable

namespace Sign_Up_Form.Migrations
{
    public partial class addingusertype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserType",
                table: "Login",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "User");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserType",
                table: "Login");
        }
    }
}
