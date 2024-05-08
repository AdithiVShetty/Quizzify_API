using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quizzify_DAL.Migrations
{
    /// <inheritdoc />
    public partial class ResponsesAndUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "QuizzifyUserResponses");

            //migrationBuilder.DropTable(
            //    name: "QuizzifyResponses");

            //migrationBuilder.CreateTable(
            //    name: "QuizzifyResponse",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        QuizId = table.Column<int>(type: "int", nullable: false),
            //        UserId = table.Column<int>(type: "int", nullable: false),
            //        TimeTaken = table.Column<TimeSpan>(type: "time", nullable: false),
            //        TotalScore = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
            //        AttemptNumber = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_QuizzifyResponse", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_QuizzifyResponse_QuizzifyQuiz_QuizId",
            //            column: x => x.QuizId,
            //            principalTable: "QuizzifyQuiz",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_QuizzifyResponse_Users_UserId",
            //            column: x => x.UserId,
            //            principalTable: "Users",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "QuizzifyUserResponse",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        QuestionId = table.Column<int>(type: "int", nullable: false),
            //        AttemptedAnswer = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        ObtainedMarks = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
            //        ResponseId = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_QuizzifyUserResponse", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_QuizzifyUserResponse_QuizzifyQuestion_QuestionId",
            //            column: x => x.QuestionId,
            //            principalTable: "QuizzifyQuestion",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_QuizzifyUserResponse_QuizzifyResponse_ResponseId",
            //            column: x => x.ResponseId,
            //            principalTable: "QuizzifyResponse",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_QuizzifyResponse_QuizId",
            //    table: "QuizzifyResponse",
            //    column: "QuizId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_QuizzifyResponse_UserId",
            //    table: "QuizzifyResponse",
            //    column: "UserId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_QuizzifyUserResponse_QuestionId",
            //    table: "QuizzifyUserResponse",
            //    column: "QuestionId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_QuizzifyUserResponse_ResponseId",
            //    table: "QuizzifyUserResponse",
            //    column: "ResponseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "QuizzifyUserResponse");

            //migrationBuilder.DropTable(
            //    name: "QuizzifyResponse");

            //migrationBuilder.CreateTable(
            //    name: "QuizzifyResponses",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        QuizId = table.Column<int>(type: "int", nullable: false),
            //        UserId = table.Column<int>(type: "int", nullable: false),
            //        AttemptNumber = table.Column<int>(type: "int", nullable: false),
            //        TimeTaken = table.Column<TimeSpan>(type: "time", nullable: false),
            //        TotalScore = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_QuizzifyResponses", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_QuizzifyResponses_QuizzifyQuiz_QuizId",
            //            column: x => x.QuizId,
            //            principalTable: "QuizzifyQuiz",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_QuizzifyResponses_Users_UserId",
            //            column: x => x.UserId,
            //            principalTable: "Users",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "QuizzifyUserResponses",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        QuestionId = table.Column<int>(type: "int", nullable: false),
            //        ResponseId = table.Column<int>(type: "int", nullable: false),
            //        AttemptedAnswer = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        ObtainedMarks = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_QuizzifyUserResponses", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_QuizzifyUserResponses_QuizzifyQuestion_QuestionId",
            //            column: x => x.QuestionId,
            //            principalTable: "QuizzifyQuestion",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_QuizzifyUserResponses_QuizzifyResponses_ResponseId",
            //            column: x => x.ResponseId,
            //            principalTable: "QuizzifyResponses",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_QuizzifyResponses_QuizId",
            //    table: "QuizzifyResponses",
            //    column: "QuizId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_QuizzifyResponses_UserId",
            //    table: "QuizzifyResponses",
            //    column: "UserId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_QuizzifyUserResponses_QuestionId",
            //    table: "QuizzifyUserResponses",
            //    column: "QuestionId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_QuizzifyUserResponses_ResponseId",
            //    table: "QuizzifyUserResponses",
            //    column: "ResponseId");
        }
    }
}
