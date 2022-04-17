using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplicationWithAuth.Data.MigrationsLKA
{
    public partial class ExamsCompetitionIDLKA : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompetitionId",
                table: "CompetitionsExams",
                maxLength: 10,
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompetitionId",
                table: "CompetitionsExams");
        }
    }
}
