using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryDAL.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Electrices_UserId",
                table: "Customers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Electrices",
                table: "Electrices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customers",
                table: "Customers");

            migrationBuilder.RenameTable(
                name: "Electrices",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "Customers",
                newName: "Bookss");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_UserId",
                table: "Bookss",
                newName: "IX_Bookss_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookss_Users_UserId",
                table: "Bookss");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bookss",
                table: "Bookss");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "Electrices");

            migrationBuilder.RenameTable(
                name: "Bookss",
                newName: "Customers");

            migrationBuilder.RenameIndex(
                name: "IX_Bookss_UserId",
                table: "Customers",
                newName: "IX_Customers_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Electrices",
                table: "Electrices",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customers",
                table: "Customers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Electrices_UserId",
                table: "Customers",
                column: "UserId",
                principalTable: "Electrices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
