using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplicationWithAuth.Data.MigrationsLKA
{
    public partial class ContractsAndAchievementsLKA : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationsAchievements",
                columns: table => new
                {
                    Id = table.Column<int>(maxLength: 10, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnrolleeId = table.Column<int>(maxLength: 10, nullable: false),
                    Year = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    EnrolleesDocId = table.Column<int>(maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationsAchievements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationsContracts",
                columns: table => new
                {
                    Id = table.Column<int>(maxLength: 10, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Client = table.Column<string>(nullable: true),
                    EnrolleeId = table.Column<int>(maxLength: 10, nullable: false),
                    EnrolleesDocId = table.Column<int>(maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationsContracts", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationsAchievements");

            migrationBuilder.DropTable(
                name: "ApplicationsContracts");
        }
    }
}
