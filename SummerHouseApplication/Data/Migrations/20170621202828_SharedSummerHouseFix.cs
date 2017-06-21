using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SummerHouseApplication.Data.Migrations
{
    public partial class SharedSummerHouseFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SharedSummerHouses_AspNetUsers_UserId1",
                table: "SharedSummerHouses");

            migrationBuilder.DropIndex(
                name: "IX_SharedSummerHouses_UserId1",
                table: "SharedSummerHouses");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "SharedSummerHouses");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "SharedSummerHouses",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_SharedSummerHouses_UserId",
                table: "SharedSummerHouses",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SharedSummerHouses_AspNetUsers_UserId",
                table: "SharedSummerHouses",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SharedSummerHouses_AspNetUsers_UserId",
                table: "SharedSummerHouses");

            migrationBuilder.DropIndex(
                name: "IX_SharedSummerHouses_UserId",
                table: "SharedSummerHouses");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "SharedSummerHouses",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "SharedSummerHouses",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SharedSummerHouses_UserId1",
                table: "SharedSummerHouses",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_SharedSummerHouses_AspNetUsers_UserId1",
                table: "SharedSummerHouses",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
