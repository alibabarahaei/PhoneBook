using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhoneBook.Infrastructure.EFCore.Migrations
{
    public partial class addUrlEmailConfirmationcolumntousertable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UrlEmailConfirmation",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UrlEmailConfirmation",
                table: "Users");
        }
    }
}
