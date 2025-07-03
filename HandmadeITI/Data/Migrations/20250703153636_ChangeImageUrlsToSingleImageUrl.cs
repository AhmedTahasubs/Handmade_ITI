using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HandmadeITI.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeImageUrlsToSingleImageUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrls",
                table: "Product",
                newName: "ImageUrl");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Product",
                newName: "ImageUrls");
        }
    }
}
