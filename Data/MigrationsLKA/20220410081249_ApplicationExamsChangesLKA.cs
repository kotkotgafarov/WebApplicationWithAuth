using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplicationWithAuth.Data.MigrationsLKA
{
    public partial class ApplicationExamsChangesLKA : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubjectName",
                table: "ApplicationsExams");

            migrationBuilder.AlterColumn<int>(
                name: "CompetitionsExamId",
                table: "ApplicationsExams",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AddColumn<int>(
                name: "EnrolleeId",
                table: "ApplicationsExams",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SubjectsSubstitutorId",
                table: "ApplicationsExams",
                maxLength: 10,
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EnrolleeId",
                table: "ApplicationsExams");

            migrationBuilder.DropColumn(
                name: "SubjectsSubstitutorId",
                table: "ApplicationsExams");

            migrationBuilder.AlterColumn<string>(
                name: "CompetitionsExamId",
                table: "ApplicationsExams",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(int),
                oldMaxLength: 10);

            migrationBuilder.AddColumn<string>(
                name: "SubjectName",
                table: "ApplicationsExams",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
