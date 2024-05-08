using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quizzify_DAL.Migrations
{
    /// <inheritdoc />
    public partial class update_new : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuizzifyQuizQuestions_QuizzifyCategory_CategoryId",
                table: "QuizzifyQuizQuestions");

            migrationBuilder.DropIndex(
                name: "IX_QuizzifyQuizQuestions_CategoryId",
                table: "QuizzifyQuizQuestions");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "QuizzifyQuizQuestions");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "QuizzifyQuiz",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "QuizCategory",
                table: "QuizzifyQuiz",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "Description",
            //    table: "QuizzifyQuiz");

            //migrationBuilder.DropColumn(
            //    name: "QuizCategory",
            //    table: "QuizzifyQuiz");

            //migrationBuilder.AddColumn<int>(
            //    name: "CategoryId",
            //    table: "QuizzifyQuizQuestions",
            //    type: "int",
            //    nullable: false,
            //    defaultValue: 0);

            //migrationBuilder.CreateIndex(
            //    name: "IX_QuizzifyQuizQuestions_CategoryId",
            //    table: "QuizzifyQuizQuestions",
            //    column: "CategoryId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_QuizzifyQuizQuestions_QuizzifyCategory_CategoryId",
            //    table: "QuizzifyQuizQuestions",
            //    column: "CategoryId",
            //    principalTable: "QuizzifyCategory",
            //    principalColumn: "Id");
        }
    }
}
