using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Computers.Migrations
{
    /// <inheritdoc />
    public partial class _20260519163325_SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PCComponents",
                keyColumns: new[] { "ComponentCode", "PCId" },
                keyValues: new object[] { "GPU001", 1 });

            migrationBuilder.InsertData(
                table: "ComponentManufacturers",
                columns: new[] { "Id", "Abbreviation", "FoundationDate", "FullName" },
                values: new object[] { 3, "Corsair", new DateTime(1994, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Corsair" });

            migrationBuilder.InsertData(
                table: "ComponentTypes",
                columns: new[] { "Id", "Abbreviation", "Name" },
                values: new object[] { 3, "RAM", "Memory" });

            migrationBuilder.InsertData(
                table: "PCs",
                columns: new[] { "Id", "CreatedAt", "Name", "Stock", "Warranty", "Weight" },
                values: new object[,]
                {
                    { 2, new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Office Pro", 10, 12, 8.20m },
                    { 3, new DateTime(2024, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Streaming Setup", 3, 36, 10.75m }
                });

            migrationBuilder.InsertData(
                table: "Components",
                columns: new[] { "Code", "ComponentManufacturerId", "ComponentTypeId", "Description", "Name" },
                values: new object[] { "RAM001", 3, 3, "DDR5 RAM Kit", "Corsair Vengeance 32GB" });

            migrationBuilder.InsertData(
                table: "PCComponents",
                columns: new[] { "ComponentCode", "PCId", "Amount" },
                values: new object[,]
                {
                    { "GPU001", 2, 1 },
                    { "RAM001", 3, 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PCComponents",
                keyColumns: new[] { "ComponentCode", "PCId" },
                keyValues: new object[] { "GPU001", 2 });

            migrationBuilder.DeleteData(
                table: "PCComponents",
                keyColumns: new[] { "ComponentCode", "PCId" },
                keyValues: new object[] { "RAM001", 3 });

            migrationBuilder.DeleteData(
                table: "Components",
                keyColumn: "Code",
                keyValue: "RAM001");

            migrationBuilder.DeleteData(
                table: "PCs",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PCs",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ComponentManufacturers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ComponentTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.InsertData(
                table: "PCComponents",
                columns: new[] { "ComponentCode", "PCId", "Amount" },
                values: new object[] { "GPU001", 1, 1 });
        }
    }
}
