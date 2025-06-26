using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RisedorApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOrderStructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Products_ProductId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_SupermarketId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_UserId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_VendorId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "VendorId",
                table: "Orders",
                newName: "SupermarketUserId");

            migrationBuilder.RenameColumn(
                name: "SupermarketId",
                table: "Orders",
                newName: "SalesRepUserId");

            migrationBuilder.RenameColumn(
                name: "OrderDate",
                table: "Orders",
                newName: "CreatedAt");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_VendorId",
                table: "Orders",
                newName: "IX_Orders_SupermarketUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_SupermarketId",
                table: "Orders",
                newName: "IX_Orders_SalesRepUserId");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "OrderItems",
                newName: "VendorId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems",
                newName: "IX_OrderItems_VendorId");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Orders",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "BuyerInfo",
                table: "Orders",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProductItemCode",
                table: "OrderItems",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Users_VendorId",
                table: "OrderItems",
                column: "VendorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_SalesRepUserId",
                table: "Orders",
                column: "SalesRepUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_SupermarketUserId",
                table: "Orders",
                column: "SupermarketUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_UserId",
                table: "Orders",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Users_VendorId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_SalesRepUserId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_SupermarketUserId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_UserId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "BuyerInfo",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ProductItemCode",
                table: "OrderItems");

            migrationBuilder.RenameColumn(
                name: "SupermarketUserId",
                table: "Orders",
                newName: "VendorId");

            migrationBuilder.RenameColumn(
                name: "SalesRepUserId",
                table: "Orders",
                newName: "SupermarketId");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Orders",
                newName: "OrderDate");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_SupermarketUserId",
                table: "Orders",
                newName: "IX_Orders_VendorId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_SalesRepUserId",
                table: "Orders",
                newName: "IX_Orders_SupermarketId");

            migrationBuilder.RenameColumn(
                name: "VendorId",
                table: "OrderItems",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItems_VendorId",
                table: "OrderItems",
                newName: "IX_OrderItems_ProductId");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Orders",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Products_ProductId",
                table: "OrderItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_SupermarketId",
                table: "Orders",
                column: "SupermarketId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_UserId",
                table: "Orders",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_VendorId",
                table: "Orders",
                column: "VendorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
