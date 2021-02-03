using Microsoft.EntityFrameworkCore.Migrations;

namespace BackEnd.DAL.Migrations
{
    public partial class Newigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Company_AspNetUsers_UserId",
                table: "Company");

            migrationBuilder.DropIndex(
                name: "IX_Company_UserId",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Company");

            migrationBuilder.AddColumn<string>(
                name: "DiscountDescription",
                table: "Discount",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "Company",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyDescription",
                table: "Company",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "tblCitiesId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "tblCountriesId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "tblCountries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    ArabicCountryName = table.Column<string>(nullable: true),
                    EnglishCountryName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCountries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblCities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    ArabicCityName = table.Column<string>(nullable: true),
                    EnglishCityName = table.Column<string>(nullable: true),
                    CountryID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblCities_tblCountries_CountryID",
                        column: x => x.CountryID,
                        principalTable: "tblCountries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Company_ApplicationUserId",
                table: "Company",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_tblCitiesId",
                table: "AspNetUsers",
                column: "tblCitiesId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_tblCountriesId",
                table: "AspNetUsers",
                column: "tblCountriesId");

            migrationBuilder.CreateIndex(
                name: "IX_tblCities_CountryID",
                table: "tblCities",
                column: "CountryID");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Company_AspNetUsers_ApplicationUserId",
                table: "Company",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_tblCities_tblCitiesId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_tblCountries_tblCountriesId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Company_AspNetUsers_ApplicationUserId",
                table: "Company");

            migrationBuilder.DropTable(
                name: "tblCities");

            migrationBuilder.DropTable(
                name: "tblCountries");

            migrationBuilder.DropIndex(
                name: "IX_Company_ApplicationUserId",
                table: "Company");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_tblCitiesId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_tblCountriesId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DiscountDescription",
                table: "Discount");

            migrationBuilder.DropColumn(
                name: "CompanyDescription",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "tblCitiesId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "tblCountriesId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "ApplicationUserId",
                table: "Company",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Company",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Company_UserId",
                table: "Company",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Company_AspNetUsers_UserId",
                table: "Company",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
