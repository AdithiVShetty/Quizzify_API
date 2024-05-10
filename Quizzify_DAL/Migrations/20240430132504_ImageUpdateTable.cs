using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quizzify_BLL.Migrations
{
    /// <inheritdoc />
    public partial class ImageUpdateTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "Url",
            //    table: "QuizzifyImage");

            //migrationBuilder.AddColumn<byte[]>(
            //    name: "Data",
            //    table: "QuizzifyImage",
            //    type: "varbinary(max)",
            //    nullable: false,
            //    defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "Data",
            //    table: "QuizzifyImage");

            //migrationBuilder.AddColumn<string>(
            //    name: "Url",
            //    table: "QuizzifyImage",
            //    type: "nvarchar(max)",
            //    nullable: false,
            //    defaultValue: "");
        }
    }
}
