using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quizzify_DAL.Migrations
{
    /// <inheritdoc />
    public partial class ImageUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.RenameColumn(
            //    name: "ContentType",
            //    table: "QuizzifyImage",
            //    newName: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.RenameColumn(
            //    name: "Name",
            //    table: "QuizzifyImage",
            //    newName: "ContentType");
        }
    }
}
