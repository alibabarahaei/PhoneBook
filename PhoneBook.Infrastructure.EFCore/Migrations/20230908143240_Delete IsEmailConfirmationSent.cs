using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhoneBook.Infrastructure.EFCore.Migrations
{
    public partial class DeleteIsEmailConfirmationSent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEmailConfirmationSent",
                table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsEmailConfirmationSent",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
