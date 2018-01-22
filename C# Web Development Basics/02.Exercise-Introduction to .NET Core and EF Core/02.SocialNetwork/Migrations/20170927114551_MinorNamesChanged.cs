using Microsoft.EntityFrameworkCore.Migrations;

namespace _02.SocialNetwork.Migrations
{
    public partial class MinorNamesChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UseRole",
                table: "UserAlbum");

            migrationBuilder.AddColumn<int>(
                name: "UserRole",
                table: "UserAlbum",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserRole",
                table: "UserAlbum");

            migrationBuilder.AddColumn<int>(
                name: "UseRole",
                table: "UserAlbum",
                nullable: false,
                defaultValue: 0);
        }
    }
}
