using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BloggerBlogKeeda.Migrations
{
    /// <inheritdoc />
    public partial class PostPublished : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "PostVisibility",
                table: "Post",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "StatusOfPost",
                table: "Post",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PostVisibility",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "StatusOfPost",
                table: "Post");
        }
    }
}
