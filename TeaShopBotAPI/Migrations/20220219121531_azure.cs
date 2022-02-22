using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeaShopBotAPI.Migrations
{
    public partial class azure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2022, 2, 19, 12, 15, 31, 11, DateTimeKind.Utc).AddTicks(8876));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2022, 2, 19, 12, 15, 31, 11, DateTimeKind.Utc).AddTicks(8879));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
