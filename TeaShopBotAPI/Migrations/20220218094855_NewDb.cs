using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeaShopBotAPI.Migrations
{
    public partial class NewDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2022, 2, 18, 9, 48, 54, 858, DateTimeKind.Utc).AddTicks(5713));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2022, 2, 18, 9, 48, 54, 858, DateTimeKind.Utc).AddTicks(5718));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2022, 2, 18, 9, 41, 20, 829, DateTimeKind.Utc).AddTicks(8208));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2022, 2, 18, 9, 41, 20, 829, DateTimeKind.Utc).AddTicks(8213));
        }
    }
}
