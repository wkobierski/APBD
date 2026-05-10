using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cwiczenia5.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ComponentManufacturer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Abbreviation = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    FullName = table.Column<string>(type: "TEXT", maxLength: 300, nullable: false),
                    FoundationDate = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentManufacturer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ComponentType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Abbreviation = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PC",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Weight = table.Column<float>(type: "float", precision: 5, nullable: false),
                    Warranty = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Stock = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PC", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Component",
                columns: table => new
                {
                    Code = table.Column<string>(type: "char", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 300, nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    ComponentManufacturersId = table.Column<int>(type: "INTEGER", nullable: false),
                    ComponentTypesId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Component", x => x.Code);
                    table.ForeignKey(
                        name: "FK_Component_ComponentManufacturer_ComponentManufacturersId",
                        column: x => x.ComponentManufacturersId,
                        principalTable: "ComponentManufacturer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Component_ComponentType_ComponentTypesId",
                        column: x => x.ComponentTypesId,
                        principalTable: "ComponentType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PCComponent",
                columns: table => new
                {
                    PcId = table.Column<int>(type: "INTEGER", nullable: false),
                    ComponentCode = table.Column<string>(type: "char", nullable: false),
                    Amount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PCComponent", x => new { x.PcId, x.ComponentCode });
                    table.ForeignKey(
                        name: "FK_PCComponent_Component_ComponentCode",
                        column: x => x.ComponentCode,
                        principalTable: "Component",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PCComponent_PC_PcId",
                        column: x => x.PcId,
                        principalTable: "PC",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Component_ComponentManufacturersId",
                table: "Component",
                column: "ComponentManufacturersId");

            migrationBuilder.CreateIndex(
                name: "IX_Component_ComponentTypesId",
                table: "Component",
                column: "ComponentTypesId");

            migrationBuilder.CreateIndex(
                name: "IX_PCComponent_ComponentCode",
                table: "PCComponent",
                column: "ComponentCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PCComponent");

            migrationBuilder.DropTable(
                name: "Component");

            migrationBuilder.DropTable(
                name: "PC");

            migrationBuilder.DropTable(
                name: "ComponentManufacturer");

            migrationBuilder.DropTable(
                name: "ComponentType");
        }
    }
}
