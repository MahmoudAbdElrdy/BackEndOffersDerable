using Microsoft.EntityFrameworkCore.Migrations;

namespace BackEnd.DAL.Migrations
{
    public partial class DiscountId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rating_Product_ProductId",
                table: "Rating");

            migrationBuilder.DropIndex(
                name: "IX_Rating_ProductId",
                table: "Rating");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Rating");

            migrationBuilder.AddColumn<int>(
                name: "DiscountId",
                table: "Rating",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rating_DiscountId",
                table: "Rating",
                column: "DiscountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_Discount_DiscountId",
                table: "Rating",
                column: "DiscountId",
                principalTable: "Discount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rating_Discount_DiscountId",
                table: "Rating");

            migrationBuilder.DropIndex(
                name: "IX_Rating_DiscountId",
                table: "Rating");

            migrationBuilder.DropColumn(
                name: "DiscountId",
                table: "Rating");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Rating",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rating_ProductId",
                table: "Rating",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_Product_ProductId",
                table: "Rating",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
