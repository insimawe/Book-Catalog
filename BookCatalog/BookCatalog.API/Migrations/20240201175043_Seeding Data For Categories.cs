using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookCatalog.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedingDataForCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("36151a6b-ba6f-45d4-9985-80a579724d46"), "Romance" },
                    { new Guid("9b37b6fb-b3b2-49ec-b359-1d913be0d822"), "Horror" },
                    { new Guid("b4b92093-03cd-4675-a7e0-1cfd76d0f321"), "Fantasy" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("36151a6b-ba6f-45d4-9985-80a579724d46"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("9b37b6fb-b3b2-49ec-b359-1d913be0d822"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b4b92093-03cd-4675-a7e0-1cfd76d0f321"));
        }
    }
}
