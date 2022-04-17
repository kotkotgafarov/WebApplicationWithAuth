using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplicationWithAuth.Data.MigrationsLKA
{
    public partial class StatusesLKA : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EnrolleesStatuses",
                columns: table => new
                {
                    EnrolleeId = table.Column<int>(maxLength: 10, nullable: false),
                    ProfileStatus = table.Column<int>(nullable: false),
                    ApplicationStatus = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnrolleesStatuses");
        }
    }
}
