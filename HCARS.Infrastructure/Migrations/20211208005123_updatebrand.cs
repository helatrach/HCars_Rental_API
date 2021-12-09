using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HCARS.Infrastructure.Migrations
{
    public partial class updatebrand : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "Brands",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Icon",
                table: "Brands");
        }
    }
}
