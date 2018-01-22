using Microsoft.EntityFrameworkCore.Migrations;

namespace _02.SocialNetwork.Migrations
{
    public partial class AddedRolesToUserAlbums : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UseRole",
                table: "UserAlbum",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UseRole",
                table: "UserAlbum");
        }
    }
}
