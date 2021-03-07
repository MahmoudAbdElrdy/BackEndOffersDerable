using Microsoft.EntityFrameworkCore.Migrations;

namespace BackEnd.DAL.Migrations
{
    public partial class discount123 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "ProductFavourite",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductFavourite_ProductId",
                table: "ProductFavourite",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductFavourite_Product_ProductId",
                table: "ProductFavourite",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductFavourite_Product_ProductId",
                table: "ProductFavourite");

            migrationBuilder.DropIndex(
                name: "IX_ProductFavourite_ProductId",
                table: "ProductFavourite");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ProductFavourite");
        }
    }
}
