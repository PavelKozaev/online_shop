using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShop.Db.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFavorites : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favorites_Products_ProductId",
                table: "Favorites");

            migrationBuilder.DropIndex(
                name: "IX_Favorites_ProductId",
                table: "Favorites");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Favorites");

            migrationBuilder.CreateTable(
                name: "FavoritesProduct",
                columns: table => new
                {
                    FavoritesId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoritesProduct", x => new { x.FavoritesId, x.ProductsId });
                    table.ForeignKey(
                        name: "FK_FavoritesProduct_Favorites_FavoritesId",
                        column: x => x.FavoritesId,
                        principalTable: "Favorites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FavoritesProduct_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FavoritesProduct_ProductsId",
                table: "FavoritesProduct",
                column: "ProductsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FavoritesProduct");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "Favorites",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_ProductId",
                table: "Favorites",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Favorites_Products_ProductId",
                table: "Favorites",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
