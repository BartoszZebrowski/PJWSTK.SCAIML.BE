using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PJWSTK.SCAIML.BE.Migrations
{
    public partial class ChangeinPostmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhotoBlobUrl",
                table: "Post",
                newName: "MainPhoto");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MainPhoto",
                table: "Post",
                newName: "PhotoBlobUrl");
        }
    }
}
