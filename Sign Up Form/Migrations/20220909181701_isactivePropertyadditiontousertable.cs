using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sign_Up_Form.Migrations
{
    public partial class isactivePropertyadditiontousertable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "UserData",
                type: "bit",
                nullable: false,
                defaultValue: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "UserData");
        }
    }
}
