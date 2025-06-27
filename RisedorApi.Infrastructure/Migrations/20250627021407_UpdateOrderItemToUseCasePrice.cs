using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RisedorApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOrderItemToUseCasePrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UnitPrice",
                table: "OrderItems",
                newName: "CasePrice");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CasePrice",
                table: "OrderItems",
                newName: "UnitPrice");
        }
    }
}
