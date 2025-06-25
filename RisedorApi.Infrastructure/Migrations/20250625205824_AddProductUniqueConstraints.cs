using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RisedorApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddProductUniqueConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Products_ItemCode",
                table: "Products",
                column: "ItemCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_UpcCode",
                table: "Products",
                column: "UpcCode",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Products_ItemCode",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_UpcCode",
                table: "Products");
        }
    }
}
