using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeaShopDAL.Migrations
{
    public partial class EditOrderModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_UserId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ProductImage",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Orders",
                newName: "UserChatId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                newName: "IX_Orders_UserChatId");

            migrationBuilder.AddColumn<string>(
                name: "ProductPathToImage",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2022, 3, 23, 11, 19, 24, 318, DateTimeKind.Utc).AddTicks(9916));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2022, 3, 23, 11, 19, 24, 318, DateTimeKind.Utc).AddTicks(9919));

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_UserChatId",
                table: "Orders",
                column: "UserChatId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_UserChatId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ProductPathToImage",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "UserChatId",
                table: "Orders",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_UserChatId",
                table: "Orders",
                newName: "IX_Orders_UserId");

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
                value: new DateTime(2022, 3, 19, 13, 46, 4, 559, DateTimeKind.Utc).AddTicks(2200));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2022, 3, 19, 13, 46, 4, 559, DateTimeKind.Utc).AddTicks(2202));

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_UserId",
                table: "Orders",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
