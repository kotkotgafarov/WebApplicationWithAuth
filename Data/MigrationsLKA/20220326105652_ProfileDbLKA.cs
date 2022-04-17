using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplicationWithAuth.Data.MigrationsLKA
{
    public partial class ProfileDbLKA : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EnrollesDocs",
                columns: table => new
                {
                    Id = table.Column<int>(maxLength: 10, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(nullable: true),
                    Series = table.Column<string>(nullable: true),
                    Number = table.Column<string>(nullable: true),
                    IssueDate = table.Column<DateTime>(nullable: false),
                    IssuedBy = table.Column<string>(nullable: true),
                    Authority = table.Column<string>(nullable: true),
                    EnrolleeID = table.Column<int>(maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrollesDocs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Relatives",
                columns: table => new
                {
                    Id = table.Column<int>(maxLength: 10, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    EnrolleeID = table.Column<int>(maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relatives", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnrollesDocs");

            migrationBuilder.DropTable(
                name: "Relatives");
        }
    }
}
