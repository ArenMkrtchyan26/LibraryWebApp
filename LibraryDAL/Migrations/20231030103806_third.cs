using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryDAL.Migrations
{
    public partial class third : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookss_Users_UserId",
                table: "Bookss");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bookss",
                table: "Bookss");

            migrationBuilder.RenameTable(
                name: "Bookss",
                newName: "Books");

            migrationBuilder.RenameIndex(
                name: "IX_Bookss_UserId",
                table: "Books",
                newName: "IX_Books_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Books",
                table: "Books",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Users_UserId",
                table: "Books",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Users_UserId",
                table: "Books");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Books",
                table: "Books");

            migrationBuilder.RenameTable(
                name: "Books",
                newName: "Bookss");

            migrationBuilder.RenameIndex(
                name: "IX_Books_UserId",
                table: "Bookss",
                newName: "IX_Bookss_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bookss",
                table: "Bookss",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookss_Users_UserId",
                table: "Bookss",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
