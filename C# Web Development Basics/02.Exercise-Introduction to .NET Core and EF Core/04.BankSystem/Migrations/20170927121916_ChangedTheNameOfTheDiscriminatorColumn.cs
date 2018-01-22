using Microsoft.EntityFrameworkCore.Migrations;

namespace _04.BankSystem.Migrations
{
    public partial class ChangedTheNameOfTheDiscriminatorColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Accounts");

            migrationBuilder.AddColumn<string>(
                name: "Account Type",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Account Type",
                table: "Accounts");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Accounts",
                nullable: false,
                defaultValue: "");
        }
    }
}
