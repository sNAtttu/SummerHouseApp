using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SummerHouseApplication.Data.Migrations
{
    public partial class MarkerRelationshipIds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Markers_Locations_CoordinatesId",
                table: "Markers");

            migrationBuilder.DropForeignKey(
                name: "FK_Markers_FishingNets_FishingNetId",
                table: "Markers");

            migrationBuilder.DropForeignKey(
                name: "FK_Markers_InfoWindows_InfoId",
                table: "Markers");

            migrationBuilder.DropForeignKey(
                name: "FK_Markers_SummerHouses_SummerHouseId",
                table: "Markers");

            migrationBuilder.AlterColumn<int>(
                name: "SummerHouseId",
                table: "Markers",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "InfoId",
                table: "Markers",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FishingNetId",
                table: "Markers",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CoordinatesId",
                table: "Markers",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Markers_Locations_CoordinatesId",
                table: "Markers",
                column: "CoordinatesId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Markers_FishingNets_FishingNetId",
                table: "Markers",
                column: "FishingNetId",
                principalTable: "FishingNets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Markers_InfoWindows_InfoId",
                table: "Markers",
                column: "InfoId",
                principalTable: "InfoWindows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Markers_SummerHouses_SummerHouseId",
                table: "Markers",
                column: "SummerHouseId",
                principalTable: "SummerHouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Markers_Locations_CoordinatesId",
                table: "Markers");

            migrationBuilder.DropForeignKey(
                name: "FK_Markers_FishingNets_FishingNetId",
                table: "Markers");

            migrationBuilder.DropForeignKey(
                name: "FK_Markers_InfoWindows_InfoId",
                table: "Markers");

            migrationBuilder.DropForeignKey(
                name: "FK_Markers_SummerHouses_SummerHouseId",
                table: "Markers");

            migrationBuilder.AlterColumn<int>(
                name: "SummerHouseId",
                table: "Markers",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "InfoId",
                table: "Markers",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "FishingNetId",
                table: "Markers",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "CoordinatesId",
                table: "Markers",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Markers_Locations_CoordinatesId",
                table: "Markers",
                column: "CoordinatesId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Markers_FishingNets_FishingNetId",
                table: "Markers",
                column: "FishingNetId",
                principalTable: "FishingNets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Markers_InfoWindows_InfoId",
                table: "Markers",
                column: "InfoId",
                principalTable: "InfoWindows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Markers_SummerHouses_SummerHouseId",
                table: "Markers",
                column: "SummerHouseId",
                principalTable: "SummerHouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
