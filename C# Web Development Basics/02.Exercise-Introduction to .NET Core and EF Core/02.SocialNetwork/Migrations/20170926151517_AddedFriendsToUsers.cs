using Microsoft.EntityFrameworkCore.Migrations;

namespace _02.SocialNetwork.Migrations
{
    public partial class AddedFriendsToUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserFriend",
                columns: table => new
                {
                    FirstUserId = table.Column<int>(type: "int", nullable: false),
                    SecondUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFriend", x => new { x.FirstUserId, x.SecondUserId });
                    table.ForeignKey(
                        name: "FK_UserFriend_Users_FirstUserId",
                        column: x => x.FirstUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserFriend_Users_SecondUserId",
                        column: x => x.SecondUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserFriend_SecondUserId",
                table: "UserFriend",
                column: "SecondUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserFriend");
        }
    }
}
