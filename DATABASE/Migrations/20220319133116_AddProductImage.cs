using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeaShopDAL.Migrations
{
    public partial class AddProductImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Products_ProductName",
                table: "Products");

            migrationBuilder.AddColumn<byte[]>(
                name: "ProductImage",
                table: "Products",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2022, 3, 19, 13, 31, 16, 552, DateTimeKind.Utc).AddTicks(3728));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2022, 3, 19, 13, 31, 16, 552, DateTimeKind.Utc).AddTicks(3733));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductImage",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2022, 3, 12, 11, 35, 6, 210, DateTimeKind.Utc).AddTicks(1070));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2022, 3, 12, 11, 35, 6, 210, DateTimeKind.Utc).AddTicks(1073));

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductName",
                table: "Products",
                column: "ProductName",
                unique: true,
                filter: "[ProductName] IS NOT NULL");
        }
    }
}
