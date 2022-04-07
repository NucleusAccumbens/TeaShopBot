using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeaShopDAL.Migrations
{
    public partial class AddImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
