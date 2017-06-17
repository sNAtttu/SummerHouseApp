using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SummerHouseApplication.Data.Migrations
{
    public partial class FishingNetNotNecessary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Markers_FishingNets_FishingNetId",
                table: "Markers");

            migrationBuilder.AlterColumn<int>(
                name: "FishingNetId",
                table: "Markers",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Markers_FishingNets_FishingNetId",
                table: "Markers",
                column: "FishingNetId",
                principalTable: "FishingNets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Markers_FishingNets_FishingNetId",
                table: "Markers");

            migrationBuilder.AlterColumn<int>(
                name: "FishingNetId",
                table: "Markers",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Markers_FishingNets_FishingNetId",
                table: "Markers",
                column: "FishingNetId",
                principalTable: "FishingNets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
