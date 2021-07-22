using Microsoft.EntityFrameworkCore.Migrations;

namespace FitnessTracker.Api.Migrations
{
    public partial class addWorkout : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WorkoutId",
                table: "Logs",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Workout",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workout", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Logs_WorkoutId",
                table: "Logs",
                column: "WorkoutId");

            migrationBuilder.AddForeignKey(
                name: "FK_Logs_Workout_WorkoutId",
                table: "Logs",
                column: "WorkoutId",
                principalTable: "Workout",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Logs_Workout_WorkoutId",
                table: "Logs");

            migrationBuilder.DropTable(
                name: "Workout");

            migrationBuilder.DropIndex(
                name: "IX_Logs_WorkoutId",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "WorkoutId",
                table: "Logs");
        }
    }
}
