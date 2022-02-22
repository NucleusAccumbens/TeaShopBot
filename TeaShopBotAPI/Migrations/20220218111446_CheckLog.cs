using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeaShopBotAPI.Migrations
{
    public partial class CheckLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2022, 2, 18, 11, 14, 45, 922, DateTimeKind.Utc).AddTicks(1165));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2022, 2, 18, 11, 14, 45, 922, DateTimeKind.Utc).AddTicks(1169));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
