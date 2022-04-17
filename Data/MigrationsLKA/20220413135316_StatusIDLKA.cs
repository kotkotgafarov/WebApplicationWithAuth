using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplicationWithAuth.Data.MigrationsLKA
{
    public partial class StatusIDLKA : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "EnrolleesStatuses",
                maxLength: 10,
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EnrolleesStatuses",
                table: "EnrolleesStatuses",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EnrolleesStatuses",
                table: "EnrolleesStatuses");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "EnrolleesStatuses");
        }
    }
}
