using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplicationExamTest.Migrations
{
    public partial class AddAdminAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CorrectedExam",
                schema: "Identity");

            migrationBuilder.DropColumn(
                name: "Mark",
                schema: "Identity",
                table: "Exam");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Mark",
                schema: "Identity",
                table: "Exam",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.CreateTable(
                name: "CorrectedExam",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Done = table.Column<bool>(type: "bit", nullable: false),
                    Mark = table.Column<float>(type: "real", nullable: false),
                    StudentAnswer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudentId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CorrectedExam", x => x.Id);
                });
        }
    }
}
