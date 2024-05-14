using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quizzify_BLL.Migrations
{
    /// <inheritdoc />
    public partial class FeedbackMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QuizzifyFeedback",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rating = table.Column<int>(type: "int", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuizId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizzifyFeedback", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuizzifyFeedback_QuizzifyQuiz_QuizId",
                        column: x => x.QuizId,
                        principalTable: "QuizzifyQuiz",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuizzifyFeedback_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuizzifyFeedback_QuizId",
                table: "QuizzifyFeedback",
                column: "QuizId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizzifyFeedback_UserId",
                table: "QuizzifyFeedback",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuizzifyFeedback");
        }
    }
}
