using Microsoft.EntityFrameworkCore.Migrations;

namespace TheSource.Data.Migrations
{
    public partial class changed_email_to_name : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Comments",
                newName: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Comments",
                newName: "Email");
        }
    }
}