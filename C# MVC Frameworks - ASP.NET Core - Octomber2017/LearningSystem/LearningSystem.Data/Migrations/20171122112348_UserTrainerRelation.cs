namespace LearningSystem.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class UserTrainerRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Articles",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 2000);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Articles",
                maxLength: 2000,
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
