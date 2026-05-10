using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace cwiczenia5.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ComponentManufacturer",
                columns: new[] { "Id", "Abbreviation", "FoundationDate", "FullName" },
                values: new object[,]
                {
                    { 1, "AMD", new DateTime(1969, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Advanced Micro Devices, Inc." },
                    { 2, "NVIDIA", new DateTime(1993, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "NVIDIA Corporation" },
                    { 3, "CORSAIR", new DateTime(1994, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Corsair Gaming, Inc." }
                });

            migrationBuilder.InsertData(
                table: "ComponentType",
                columns: new[] { "Id", "Abbreviation", "Name" },
                values: new object[,]
                {
                    { 1, "CPU", "Central Processing Unit" },
                    { 2, "GPU", "Graphics Processing Unit" },
                    { 3, "RAM", "Random Access Memory" }
                });

            migrationBuilder.InsertData(
                table: "PC",
                columns: new[] { "Id", "CreatedAt", "Name", "Stock", "Warranty", "Weight" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Gaming Rig X1", 10, 24, 12.5f },
                    { 2, new DateTime(2025, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Office Pro M2", 25, 12, 8f },
                    { 3, new DateTime(2025, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Workstation Z3", 5, 36, 15.2f }
                });

            migrationBuilder.InsertData(
                table: "Component",
                columns: new[] { "Code", "ComponentManufacturersId", "ComponentTypesId", "Description", "Name" },
                values: new object[,]
                {
                    { "CPU0000001", 1, 1, "8-core desktop processor", "Ryzen 7 7700X" },
                    { "GPU0000001", 2, 2, "Mid-range gaming GPU", "GeForce RTX 4070" },
                    { "RAM0000001", 3, 3, "DDR5 5600 MHz memory module", "Vengeance 16GB DDR5" }
                });

            migrationBuilder.InsertData(
                table: "PCComponent",
                columns: new[] { "ComponentCode", "PcId", "Amount" },
                values: new object[,]
                {
                    { "CPU0000001", 1, 1 },
                    { "GPU0000001", 2, 1 },
                    { "RAM0000001", 3, 4 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PCComponent",
                keyColumns: new[] { "ComponentCode", "PcId" },
                keyValues: new object[] { "CPU0000001", 1 });

            migrationBuilder.DeleteData(
                table: "PCComponent",
                keyColumns: new[] { "ComponentCode", "PcId" },
                keyValues: new object[] { "GPU0000001", 2 });

            migrationBuilder.DeleteData(
                table: "PCComponent",
                keyColumns: new[] { "ComponentCode", "PcId" },
                keyValues: new object[] { "RAM0000001", 3 });

            migrationBuilder.DeleteData(
                table: "Component",
                keyColumn: "Code",
                keyValue: "CPU0000001");

            migrationBuilder.DeleteData(
                table: "Component",
                keyColumn: "Code",
                keyValue: "GPU0000001");

            migrationBuilder.DeleteData(
                table: "Component",
                keyColumn: "Code",
                keyValue: "RAM0000001");

            migrationBuilder.DeleteData(
                table: "PC",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PC",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PC",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ComponentManufacturer",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ComponentManufacturer",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ComponentManufacturer",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ComponentType",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ComponentType",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ComponentType",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
