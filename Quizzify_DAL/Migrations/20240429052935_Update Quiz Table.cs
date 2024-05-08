using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quizzify_DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateQuizTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Question_Category_CategoryId",
                table: "Question");

            migrationBuilder.DropForeignKey(
                name: "FK_Question_Image_ImageId",
                table: "Question");

            migrationBuilder.DropForeignKey(
                name: "FK_Question_QuestionType_QuestionTypeId",
                table: "Question");

            migrationBuilder.DropForeignKey(
                name: "FK_QuizzifyQuizQuestions_Category_CategoryId",
                table: "QuizzifyQuizQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_QuizzifyQuizQuestions_Question_QuestionId",
                table: "QuizzifyQuizQuestions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuestionType",
                table: "QuestionType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Question",
                table: "Question");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Image",
                table: "Image");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                table: "Category");

            migrationBuilder.RenameTable(
                name: "QuestionType",
                newName: "QuizzifyQuestionType");

            migrationBuilder.RenameTable(
                name: "Question",
                newName: "QuizzifyQuestion");

            migrationBuilder.RenameTable(
                name: "Image",
                newName: "QuizzifyImage");

            migrationBuilder.RenameTable(
                name: "Category",
                newName: "QuizzifyCategory");

            migrationBuilder.RenameIndex(
                name: "IX_Question_QuestionTypeId",
                table: "QuizzifyQuestion",
                newName: "IX_QuizzifyQuestion_QuestionTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Question_ImageId",
                table: "QuizzifyQuestion",
                newName: "IX_QuizzifyQuestion_ImageId");

            migrationBuilder.RenameIndex(
                name: "IX_Question_CategoryId",
                table: "QuizzifyQuestion",
                newName: "IX_QuizzifyQuestion_CategoryId");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "QuizzifyQuestion",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuizzifyQuestionType",
                table: "QuizzifyQuestionType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuizzifyQuestion",
                table: "QuizzifyQuestion",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuizzifyImage",
                table: "QuizzifyImage",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuizzifyCategory",
                table: "QuizzifyCategory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuizzifyQuestion_QuizzifyCategory_CategoryId",
                table: "QuizzifyQuestion",
                column: "CategoryId",
                principalTable: "QuizzifyCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuizzifyQuestion_QuizzifyImage_ImageId",
                table: "QuizzifyQuestion",
                column: "ImageId",
                principalTable: "QuizzifyImage",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuizzifyQuestion_QuizzifyQuestionType_QuestionTypeId",
                table: "QuizzifyQuestion",
                column: "QuestionTypeId",
                principalTable: "QuizzifyQuestionType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuizzifyQuizQuestions_QuizzifyCategory_CategoryId",
                table: "QuizzifyQuizQuestions",
                column: "CategoryId",
                principalTable: "QuizzifyCategory",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuizzifyQuizQuestions_QuizzifyQuestion_QuestionId",
                table: "QuizzifyQuizQuestions",
                column: "QuestionId",
                principalTable: "QuizzifyQuestion",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuizzifyQuestion_QuizzifyCategory_CategoryId",
                table: "QuizzifyQuestion");

            migrationBuilder.DropForeignKey(
                name: "FK_QuizzifyQuestion_QuizzifyImage_ImageId",
                table: "QuizzifyQuestion");

            migrationBuilder.DropForeignKey(
                name: "FK_QuizzifyQuestion_QuizzifyQuestionType_QuestionTypeId",
                table: "QuizzifyQuestion");

            migrationBuilder.DropForeignKey(
                name: "FK_QuizzifyQuizQuestions_QuizzifyCategory_CategoryId",
                table: "QuizzifyQuizQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_QuizzifyQuizQuestions_QuizzifyQuestion_QuestionId",
                table: "QuizzifyQuizQuestions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuizzifyQuestionType",
                table: "QuizzifyQuestionType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuizzifyQuestion",
                table: "QuizzifyQuestion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuizzifyImage",
                table: "QuizzifyImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuizzifyCategory",
                table: "QuizzifyCategory");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "QuizzifyQuestion");

            migrationBuilder.RenameTable(
                name: "QuizzifyQuestionType",
                newName: "QuestionType");

            migrationBuilder.RenameTable(
                name: "QuizzifyQuestion",
                newName: "Question");

            migrationBuilder.RenameTable(
                name: "QuizzifyImage",
                newName: "Image");

            migrationBuilder.RenameTable(
                name: "QuizzifyCategory",
                newName: "Category");

            migrationBuilder.RenameIndex(
                name: "IX_QuizzifyQuestion_QuestionTypeId",
                table: "Question",
                newName: "IX_Question_QuestionTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_QuizzifyQuestion_ImageId",
                table: "Question",
                newName: "IX_Question_ImageId");

            migrationBuilder.RenameIndex(
                name: "IX_QuizzifyQuestion_CategoryId",
                table: "Question",
                newName: "IX_Question_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuestionType",
                table: "QuestionType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Question",
                table: "Question",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Image",
                table: "Image",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                table: "Category",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Question_Category_CategoryId",
                table: "Question",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Question_Image_ImageId",
                table: "Question",
                column: "ImageId",
                principalTable: "Image",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Question_QuestionType_QuestionTypeId",
                table: "Question",
                column: "QuestionTypeId",
                principalTable: "QuestionType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuizzifyQuizQuestions_Category_CategoryId",
                table: "QuizzifyQuizQuestions",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuizzifyQuizQuestions_Question_QuestionId",
                table: "QuizzifyQuizQuestions",
                column: "QuestionId",
                principalTable: "Question",
                principalColumn: "Id");
        }
    }
}
