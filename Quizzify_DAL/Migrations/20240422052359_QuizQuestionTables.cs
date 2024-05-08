using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quizzify_DAL.Migrations
{
    /// <inheritdoc />
    public partial class QuizQuestionTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "Category",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Category", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Image",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Data = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
            //        ContentType = table.Column<string>(type: "nvarchar(max)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Image", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "QuestionType",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_QuestionType", x => x.Id);
            //    });

            migrationBuilder.CreateTable(
                name: "QuizzifyQuiz",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AutoValidation = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: false),
                    Level = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalMarks = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalQuestion = table.Column<int>(type: "int", nullable: false),
                    IsEnable = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizzifyQuiz", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuizzifyQuiz_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            //migrationBuilder.CreateTable(
            //    name: "Question",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        QuestionText = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        ImageId = table.Column<int>(type: "int", nullable: true),
            //        QuestionTypeId = table.Column<int>(type: "int", nullable: false),
            //        CategoryId = table.Column<int>(type: "int", nullable: false),
            //        CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        IsEnable = table.Column<bool>(type: "bit", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Question", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Question_Category_CategoryId",
            //            column: x => x.CategoryId,
            //            principalTable: "Category",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_Question_Image_ImageId",
            //            column: x => x.ImageId,
            //            principalTable: "Image",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_Question_QuestionType_QuestionTypeId",
            //            column: x => x.QuestionTypeId,
            //            principalTable: "QuestionType",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            migrationBuilder.CreateTable(
                name: "QuizzifyQuizQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuizId = table.Column<int>(type: "int", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Marks = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizzifyQuizQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuizzifyQuizQuestions_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_QuizzifyQuizQuestions_Question_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Question",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_QuizzifyQuizQuestions_QuizzifyQuiz_QuizId",
                        column: x => x.QuizId,
                        principalTable: "QuizzifyQuiz",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Question_CategoryId",
                table: "Question",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Question_ImageId",
                table: "Question",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Question_QuestionTypeId",
                table: "Question",
                column: "QuestionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizzifyQuiz_UserId",
                table: "QuizzifyQuiz",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizzifyQuizQuestions_CategoryId",
                table: "QuizzifyQuizQuestions",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizzifyQuizQuestions_QuestionId",
                table: "QuizzifyQuizQuestions",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizzifyQuizQuestions_QuizId",
                table: "QuizzifyQuizQuestions",
                column: "QuizId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuizzifyQuizQuestions");

            migrationBuilder.DropTable(
                name: "Question");

            migrationBuilder.DropTable(
                name: "QuizzifyQuiz");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.DropTable(
                name: "QuestionType");
        }
    }
}
