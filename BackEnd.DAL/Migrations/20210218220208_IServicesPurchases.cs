using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BackEnd.DAL.Migrations
{
    public partial class IServicesPurchases : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PurchasesDate",
                table: "Purchases");

            migrationBuilder.AddColumn<DateTime>(
                name: "PurchaseDate",
                table: "Purchases",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SaleDate",
                table: "Purchases",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "quantity",
                table: "Purchases",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PurchaseDate",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "SaleDate",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "quantity",
                table: "Purchases");

            migrationBuilder.AddColumn<DateTime>(
                name: "PurchasesDate",
                table: "Purchases",
                type: "datetime2",
                nullable: true);
        }
    }
}
