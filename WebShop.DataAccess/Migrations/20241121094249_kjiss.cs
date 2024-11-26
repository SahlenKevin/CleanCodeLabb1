using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebShop.Repository.Migrations
{
    /// <inheritdoc />
    public partial class kjiss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "OrderProduct",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrderId1",
                table: "OrderProduct",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductId1",
                table: "OrderProduct",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderProduct_OrderId1",
                table: "OrderProduct",
                column: "OrderId1");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProduct_ProductId1",
                table: "OrderProduct",
                column: "ProductId1");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProduct_Orders_OrderId1",
                table: "OrderProduct",
                column: "OrderId1",
                principalTable: "Orders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProduct_Products_ProductId1",
                table: "OrderProduct",
                column: "ProductId1",
                principalTable: "Products",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderProduct_Orders_OrderId1",
                table: "OrderProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderProduct_Products_ProductId1",
                table: "OrderProduct");

            migrationBuilder.DropIndex(
                name: "IX_OrderProduct_OrderId1",
                table: "OrderProduct");

            migrationBuilder.DropIndex(
                name: "IX_OrderProduct_ProductId1",
                table: "OrderProduct");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "OrderProduct");

            migrationBuilder.DropColumn(
                name: "OrderId1",
                table: "OrderProduct");

            migrationBuilder.DropColumn(
                name: "ProductId1",
                table: "OrderProduct");
        }
    }
}
