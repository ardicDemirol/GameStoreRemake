using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GameStoreRemake.Migrations
{
    /// <inheritdoc />
    public partial class seedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "Genre", "ImageUri", "Name", "Price", "ReleaseDate" },
                values: new object[,]
                {
                    { 1, "Fighting", "https://placehold.co/100", "Street Fighter II", 19.99m, new DateTime(1991, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "Platformer", "https://placehold.co/100", "Super Mario Bros.", 29.99m, new DateTime(1985, 9, 13, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "Action-Adventure", "https://placehold.co/100", "The Legend of Zelda", 39.99m, new DateTime(1986, 2, 21, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
