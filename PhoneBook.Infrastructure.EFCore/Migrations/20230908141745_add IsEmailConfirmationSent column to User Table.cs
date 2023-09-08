using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhoneBook.Infrastructure.EFCore.Migrations
{
    public partial class addIsEmailConfirmationSentcolumntoUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsEmailConfirmationSent",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEmailConfirmationSent",
                table: "Users");
        }
    }
}
