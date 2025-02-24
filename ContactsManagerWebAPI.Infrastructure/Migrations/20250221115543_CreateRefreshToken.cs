using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ContactsManagerWebAPI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateRefreshToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "CityId",
                keyValue: new Guid("88c06874-6a24-4303-a392-60deb14f0f8c"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "CityId",
                keyValue: new Guid("8da729e6-c45f-43fc-a1d3-a05a1453a4a4"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "CityId",
                keyValue: new Guid("9b830c37-a3b1-4a21-b825-f171be7d8a93"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "CityId",
                keyValue: new Guid("ae427c99-f716-44fc-98ef-215397d35b19"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "CityId",
                keyValue: new Guid("ca1a181c-7fe4-4788-b21e-5c89d4de25fe"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "CityId",
                keyValue: new Guid("e9ec1015-3719-4b22-86f2-97fafd24a74f"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "CityId",
                keyValue: new Guid("f3dde10f-cfda-4f5b-954e-55007660d403"));

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "CityId", "CityArea", "CityName", "CityPopulation" },
                values: new object[,]
                {
                    { new Guid("88c06874-6a24-4303-a392-60deb14f0f8c"), 0.0, "Lagos", 15000000 },
                    { new Guid("8da729e6-c45f-43fc-a1d3-a05a1453a4a4"), 0.0, "Nairobi", 4500000 },
                    { new Guid("9b830c37-a3b1-4a21-b825-f171be7d8a93"), 0.0, "Casablanca", 3400000 },
                    { new Guid("ae427c99-f716-44fc-98ef-215397d35b19"), 0.0, "Addis-Abeba", 5000000 },
                    { new Guid("ca1a181c-7fe4-4788-b21e-5c89d4de25fe"), 0.0, "Alger", 3500000 },
                    { new Guid("e9ec1015-3719-4b22-86f2-97fafd24a74f"), 0.0, "Le Caire", 10000000 },
                    { new Guid("f3dde10f-cfda-4f5b-954e-55007660d403"), 0.0, "Johannesburg", 5700000 }
                });
        }
    }
}
