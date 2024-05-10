using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quizzify_BLL.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "Organisations",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Organisations", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "QuizzifyCategory",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_QuizzifyCategory", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "QuizzifyImage",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Data = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
            //        ContentType = table.Column<string>(type: "nvarchar(max)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_QuizzifyImage", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "QuizzifyQuestionType",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_QuizzifyQuestionType", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Roles",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Roles", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Users",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        EmailId = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        IsApproved = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
            //        IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
            //        ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        OrganisationId = table.Column<int>(type: "int", nullable: false),
            //        RoleId = table.Column<int>(type: "int", nullable: false, defaultValue: 3)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Users", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Users_Organisations_OrganisationId",
            //            column: x => x.OrganisationId,
            //            principalTable: "Organisations",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_Users_Roles_RoleId",
            //            column: x => x.RoleId,
            //            principalTable: "Roles",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "QuizzifyQuestion",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        QuestionText = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        ImageId = table.Column<int>(type: "int", nullable: true),
            //        QuestionTypeId = table.Column<int>(type: "int", nullable: false),
            //        UserId = table.Column<int>(type: "int", nullable: false),
            //        CategoryId = table.Column<int>(type: "int", nullable: false),
            //        CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        IsEnable = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_QuizzifyQuestion", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_QuizzifyQuestion_QuizzifyCategory_CategoryId",
            //            column: x => x.CategoryId,
            //            principalTable: "QuizzifyCategory",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_QuizzifyQuestion_QuizzifyImage_ImageId",
            //            column: x => x.ImageId,
            //            principalTable: "QuizzifyImage",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_QuizzifyQuestion_QuizzifyQuestionType_QuestionTypeId",
            //            column: x => x.QuestionTypeId,
            //            principalTable: "QuizzifyQuestionType",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_QuizzifyQuestion_Users_UserId",
            //            column: x => x.UserId,
            //            principalTable: "Users",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "QuizzifyAnswer",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        QuestionId = table.Column<int>(type: "int", nullable: false),
            //        OptionsAnswers = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        ImageId = table.Column<int>(type: "int", nullable: false),
            //        IsCorrect = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_QuizzifyAnswer", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_QuizzifyAnswer_QuizzifyImage_ImageId",
            //            column: x => x.ImageId,
            //            principalTable: "QuizzifyImage",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_QuizzifyAnswer_QuizzifyQuestion_QuestionId",
            //            column: x => x.QuestionId,
            //            principalTable: "QuizzifyQuestion",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_QuizzifyAnswer_ImageId",
            //    table: "QuizzifyAnswer",
            //    column: "ImageId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_QuizzifyAnswer_QuestionId",
            //    table: "QuizzifyAnswer",
            //    column: "QuestionId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_QuizzifyQuestion_CategoryId",
            //    table: "QuizzifyQuestion",
            //    column: "CategoryId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_QuizzifyQuestion_ImageId",
            //    table: "QuizzifyQuestion",
            //    column: "ImageId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_QuizzifyQuestion_QuestionTypeId",
            //    table: "QuizzifyQuestion",
            //    column: "QuestionTypeId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_QuizzifyQuestion_UserId",
            //    table: "QuizzifyQuestion",
            //    column: "UserId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Users_OrganisationId",
            //    table: "Users",
            //    column: "OrganisationId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Users_RoleId",
            //    table: "Users",
            //    column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "QuizzifyAnswer");

            //migrationBuilder.DropTable(
            //    name: "QuizzifyQuestion");

            //migrationBuilder.DropTable(
            //    name: "QuizzifyCategory");

            //migrationBuilder.DropTable(
            //    name: "QuizzifyImage");

            //migrationBuilder.DropTable(
            //    name: "QuizzifyQuestionType");

            //migrationBuilder.DropTable(
            //    name: "Users");

            //migrationBuilder.DropTable(
            //    name: "Organisations");

            //migrationBuilder.DropTable(
            //    name: "Roles");
        }
    }
}
