using Microsoft.EntityFrameworkCore.Migrations;

namespace MyProject.Migrations
{
    public partial class AddedExercisesCourses_CourseLecturers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CourseLecturers",
                columns: table => new
                {
                    CourseId = table.Column<int>(nullable: false),
                    LecturerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseLecturers", x => new { x.CourseId, x.LecturerId });
                });

            migrationBuilder.CreateTable(
                name: "ExercisesCourses",
                columns: table => new
                {
                    ExId = table.Column<int>(nullable: false),
                    CourseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExercisesCourses", x => new { x.CourseId, x.ExId });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseLecturers");

            migrationBuilder.DropTable(
                name: "ExercisesCourses");
        }
    }
}
