using Microsoft.EntityFrameworkCore.Migrations;

namespace FitnessTracker.Api.Migrations
{
    public partial class updateModelBuilder1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_LogExercises_LogId_ExerciseId",
                table: "LogExercises",
                columns: new[] { "LogId", "ExerciseId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LogExercises_LogId_ExerciseId",
                table: "LogExercises");
        }
    }
}
