using Microsoft.EntityFrameworkCore.Migrations;

namespace BackEnd.DAL.Migrations
{
    public partial class City : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_tblCities_tblCitiesId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_tblCountries_tblCountriesId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_tblCitiesId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_tblCountriesId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "tblCitiesId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "tblCountriesId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "tblCitiesId",
                table: "Product",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "tblCountriesId",
                table: "Product",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_tblCitiesId",
                table: "Product",
                column: "tblCitiesId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_tblCountriesId",
                table: "Product",
                column: "tblCountriesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_tblCities_tblCitiesId",
                table: "Product",
                column: "tblCitiesId",
                principalTable: "tblCities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_tblCountries_tblCountriesId",
                table: "Product",
                column: "tblCountriesId",
                principalTable: "tblCountries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_tblCities_tblCitiesId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_tblCountries_tblCountriesId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_tblCitiesId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_tblCountriesId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "tblCitiesId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "tblCountriesId",
                table: "Product");

            migrationBuilder.AddColumn<int>(
                name: "tblCitiesId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "tblCountriesId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_tblCitiesId",
                table: "AspNetUsers",
                column: "tblCitiesId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_tblCountriesId",
                table: "AspNetUsers",
                column: "tblCountriesId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_tblCities_tblCitiesId",
                table: "AspNetUsers",
                column: "tblCitiesId",
                principalTable: "tblCities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_tblCountries_tblCountriesId",
                table: "AspNetUsers",
                column: "tblCountriesId",
                principalTable: "tblCountries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
