using Microsoft.EntityFrameworkCore.Migrations;

namespace TheSource.Data.Migrations
{
    public partial class third : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "title",
                table: "Post",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Post",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "post",
                table: "Post",
                newName: "Body");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Post",
                newName: "title");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Post",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Body",
                table: "Post",
                newName: "post");
        }
    }
}
