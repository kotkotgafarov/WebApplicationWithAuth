using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplicationWithAuth.Data.MigrationsLKA
{
    public partial class ExamsLKA : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationsExams",
                columns: table => new
                {
                    Id = table.Column<int>(maxLength: 10, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompetitionsExamId = table.Column<string>(maxLength: 10, nullable: false),
                    Type = table.Column<int>(nullable: false),
                    SubjectId = table.Column<int>(maxLength: 10, nullable: false),
                    SubjectName = table.Column<string>(nullable: true),
                    Score = table.Column<int>(nullable: false),
                    EnrolleesDocId = table.Column<int>(maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationsExams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompetitionsExams",
                columns: table => new
                {
                    Id = table.Column<int>(maxLength: 10, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MinScore = table.Column<int>(nullable: false),
                    MaxScore = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    SubjectId = table.Column<int>(maxLength: 10, nullable: false),
                    egeIsFeasible = table.Column<bool>(nullable: false),
                    examIsFeasible = table.Column<bool>(nullable: false),
                    achievementIsFeasible = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetitionsExams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    Id = table.Column<int>(maxLength: 10, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubjectSubstitutors",
                columns: table => new
                {
                    SubjectId = table.Column<int>(maxLength: 10, nullable: false),
                    SubjectsSubstitutorId = table.Column<int>(maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationsExams");

            migrationBuilder.DropTable(
                name: "CompetitionsExams");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "SubjectSubstitutors");
        }
    }
}
