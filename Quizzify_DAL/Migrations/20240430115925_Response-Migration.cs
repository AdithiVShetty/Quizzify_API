using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quizzify_BLL.Migrations
{
    /// <inheritdoc />
    public partial class ResponseMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuizzifyAnswer_QuizzifyImage_ImageId",
                table: "QuizzifyAnswer");

            migrationBuilder.DropColumn(
                name: "Data",
                table: "QuizzifyImage");

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "QuizzifyImage",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "ImageId",
                table: "QuizzifyAnswer",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "QuizzifyUserResponse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    AttemptedAnswer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ObtainedMarks = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizzifyUserResponse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuizzifyUserResponse_QuizzifyQuestion_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "QuizzifyQuestion",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "QuizzifyResponse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuizId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    UserResponseId = table.Column<int>(type: "int", nullable: false),
                    AttemptCount = table.Column<int>(type: "int", nullable: false),
                    TotalScore = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizzifyResponse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuizzifyResponse_QuizzifyQuiz_QuizId",
                        column: x => x.QuizId,
                        principalTable: "QuizzifyQuiz",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_QuizzifyResponse_QuizzifyUserResponse_UserResponseId",
                        column: x => x.UserResponseId,
                        principalTable: "QuizzifyUserResponse",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_QuizzifyResponse_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuizzifyResponse_QuizId",
                table: "QuizzifyResponse",
                column: "QuizId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizzifyResponse_UserId",
                table: "QuizzifyResponse",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizzifyResponse_UserResponseId",
                table: "QuizzifyResponse",
                column: "UserResponseId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizzifyUserResponse_QuestionId",
                table: "QuizzifyUserResponse",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuizzifyAnswer_QuizzifyImage_ImageId",
                table: "QuizzifyAnswer",
                column: "ImageId",
                principalTable: "QuizzifyImage",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuizzifyAnswer_QuizzifyImage_ImageId",
                table: "QuizzifyAnswer");

            migrationBuilder.DropTable(
                name: "QuizzifyResponse");

            migrationBuilder.DropTable(
                name: "QuizzifyUserResponse");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "QuizzifyImage");

            migrationBuilder.AddColumn<byte[]>(
                name: "Data",
                table: "QuizzifyImage",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AlterColumn<int>(
                name: "ImageId",
                table: "QuizzifyAnswer",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_QuizzifyAnswer_QuizzifyImage_ImageId",
                table: "QuizzifyAnswer",
                column: "ImageId",
                principalTable: "QuizzifyImage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
