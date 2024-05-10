using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quizzify_DAL.Migrations
{
    /// <inheritdoc />
    public partial class feedback : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AlterColumn<int>(
            //    name: "RoleId",
            //    table: "Users",
            //    type: "int",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "int",
            //    oldDefaultValue: 3);

            //migrationBuilder.AlterColumn<bool>(
            //    name: "IsApproved",
            //    table: "Users",
            //    type: "bit",
            //    nullable: false,
            //    oldClrType: typeof(bool),
            //    oldType: "bit",
            //    oldDefaultValue: false);

            //migrationBuilder.AlterColumn<bool>(
            //    name: "IsActive",
            //    table: "Users",
            //    type: "bit",
            //    nullable: false,
            //    oldClrType: typeof(bool),
            //    oldType: "bit",
            //    oldDefaultValue: true);

            //migrationBuilder.CreateTable(
            //    name: "Quiz",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        UserId = table.Column<int>(type: "int", nullable: false),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        QuizCategory = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        AutoValidation = table.Column<bool>(type: "bit", nullable: false),
            //        StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        Duration = table.Column<TimeSpan>(type: "time", nullable: false),
            //        Level = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        TotalMarks = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
            //        TotalQuestion = table.Column<int>(type: "int", nullable: false),
            //        IsEnable = table.Column<bool>(type: "bit", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Quiz", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Quiz_Users_UserId",
            //            column: x => x.UserId,
            //            principalTable: "Users",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            migrationBuilder.CreateTable(
                name: "Feedbacks",
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
                    table.PrimaryKey("PK_Feedbacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Feedbacks_Quiz_QuizId",
                        column: x => x.QuizId,
                        principalTable: "QuizzifyQuiz",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Feedbacks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_QuizId",
                table: "Feedbacks",
                column: "QuizId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_UserId",
                table: "Feedbacks",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Quiz_UserId",
                table: "QuizzifyQuiz",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Feedbacks");

            //    migrationBuilder.DropTable(
            //        name: "Quiz");

            //    migrationBuilder.AlterColumn<int>(
            //        name: "RoleId",
            //        table: "Users",
            //        type: "int",
            //        nullable: false,
            //        defaultValue: 3,
            //        oldClrType: typeof(int),
            //        oldType: "int");

            //    migrationBuilder.AlterColumn<bool>(
            //        name: "IsApproved",
            //        table: "Users",
            //        type: "bit",
            //        nullable: false,
            //        defaultValue: false,
            //        oldClrType: typeof(bool),
            //        oldType: "bit");

            //    migrationBuilder.AlterColumn<bool>(
            //        name: "IsActive",
            //        table: "Users",
            //        type: "bit",
            //        nullable: false,
            //        defaultValue: true,
            //        oldClrType: typeof(bool),
            //        oldType: "bit");
            //}
        }
    }
}
