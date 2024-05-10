using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShop.Db.Migrations
{
    /// <inheritdoc />
    public partial class ModifyProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Favorites",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Carts",
                newName: "UserName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Favorites",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Carts",
                newName: "UserId");
        }
    }
}
