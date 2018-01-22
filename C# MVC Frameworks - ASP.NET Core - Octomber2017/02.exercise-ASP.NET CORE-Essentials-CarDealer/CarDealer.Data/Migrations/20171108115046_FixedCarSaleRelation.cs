namespace CarDealer.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class FixedCarSaleRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SaleId",
                table: "Cars");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SaleId",
                table: "Cars",
                nullable: false,
                defaultValue: 0);
        }
    }
}
