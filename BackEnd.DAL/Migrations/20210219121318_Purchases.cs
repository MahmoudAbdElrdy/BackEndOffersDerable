using Microsoft.EntityFrameworkCore.Migrations;

namespace BackEnd.DAL.Migrations
{
    public partial class Purchases : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DiscountId",
                table: "Purchases",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "NewPrice",
                table: "Purchases",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountId",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "NewPrice",
                table: "Purchases");
        }
    }
}
