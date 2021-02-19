using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BackEnd.DAL.Migrations
{
    public partial class Client : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Client_AspNetUsers_UserId",
                table: "Client");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Discount_DiscountId",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_DiscountId",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "IX_Client_UserId",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "DiscountId",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Client");

            migrationBuilder.AddColumn<string>(
                name: "DiscountDescription",
                table: "Purchases",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "DiscountRate",
                table: "Purchases",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DiscountType",
                table: "Purchases",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "DiscountValue",
                table: "Purchases",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Purchases",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Purchases",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PurchasesDate",
                table: "Purchases",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SatrtDate",
                table: "Purchases",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "Client",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_ProductId",
                table: "Purchases",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Client_ApplicationUserId",
                table: "Client",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Client_AspNetUsers_ApplicationUserId",
                table: "Client",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Product_ProductId",
                table: "Purchases",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Client_AspNetUsers_ApplicationUserId",
                table: "Client");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Product_ProductId",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_ProductId",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "IX_Client_ApplicationUserId",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "DiscountDescription",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "DiscountRate",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "DiscountType",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "DiscountValue",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "PurchasesDate",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "SatrtDate",
                table: "Purchases");

            migrationBuilder.AddColumn<int>(
                name: "DiscountId",
                table: "Purchases",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ApplicationUserId",
                table: "Client",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Latitude",
                table: "Client",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Longitude",
                table: "Client",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Client",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_DiscountId",
                table: "Purchases",
                column: "DiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_Client_UserId",
                table: "Client",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Client_AspNetUsers_UserId",
                table: "Client",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Discount_DiscountId",
                table: "Purchases",
                column: "DiscountId",
                principalTable: "Discount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
