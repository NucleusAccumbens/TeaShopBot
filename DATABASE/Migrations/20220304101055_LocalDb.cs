using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeaShopDAL.Migrations
{
    public partial class LocalDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2022, 3, 4, 10, 10, 55, 144, DateTimeKind.Utc).AddTicks(2739));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2022, 3, 4, 10, 10, 55, 144, DateTimeKind.Utc).AddTicks(2742));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2022, 2, 23, 19, 2, 52, 312, DateTimeKind.Utc).AddTicks(4968));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2022, 2, 23, 19, 2, 52, 312, DateTimeKind.Utc).AddTicks(4971));
        }
    }
}
