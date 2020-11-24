using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SpendingControl.API.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Incomes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Value = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incomes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WhereSpents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WhereSpents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FixedSpends",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    WhereId = table.Column<int>(type: "INTEGER", nullable: false),
                    WhereSpentId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FixedSpends", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FixedSpends_WhereSpents_WhereSpentId",
                        column: x => x.WhereSpentId,
                        principalTable: "WhereSpents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InstalledSpends",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Cost = table.Column<decimal>(type: "TEXT", nullable: false),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    QtyInstallments = table.Column<int>(type: "INTEGER", nullable: false),
                    WhereId = table.Column<int>(type: "INTEGER", nullable: false),
                    WhereSpentId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstalledSpends", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InstalledSpends_WhereSpents_WhereSpentId",
                        column: x => x.WhereSpentId,
                        principalTable: "WhereSpents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FixedSpends_WhereSpentId",
                table: "FixedSpends",
                column: "WhereSpentId");

            migrationBuilder.CreateIndex(
                name: "IX_InstalledSpends_WhereSpentId",
                table: "InstalledSpends",
                column: "WhereSpentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FixedSpends");

            migrationBuilder.DropTable(
                name: "Incomes");

            migrationBuilder.DropTable(
                name: "InstalledSpends");

            migrationBuilder.DropTable(
                name: "WhereSpents");
        }
    }
}
