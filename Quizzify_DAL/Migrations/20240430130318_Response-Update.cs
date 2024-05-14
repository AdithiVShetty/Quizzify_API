using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quizzify_BLL.Migrations
{
    /// <inheritdoc />
    public partial class ResponseUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.RenameColumn(
            //    name: "AttemptCount",
            //    table: "QuizzifyResponse",
            //    newName: "AttemptNumber");

            //migrationBuilder.AlterColumn<decimal>(
            //    name: "ObtainedMarks",
            //    table: "QuizzifyUserResponse",
            //    type: "decimal(18,2)",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "int");

            //migrationBuilder.AddColumn<TimeSpan>(
            //    name: "TimeTaken",
            //    table: "QuizzifyResponse",
            //    type: "time",
            //    nullable: false,
            //    defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "TimeTaken",
            //    table: "QuizzifyResponse");

            //migrationBuilder.RenameColumn(
            //    name: "AttemptNumber",
            //    table: "QuizzifyResponse",
            //    newName: "AttemptCount");

            //migrationBuilder.AlterColumn<int>(
            //    name: "ObtainedMarks",
            //    table: "QuizzifyUserResponse",
            //    type: "int",
            //    nullable: false,
            //    oldClrType: typeof(decimal),
            //    oldType: "decimal(18,2)");
        }
    }
}
