using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SummerHouseApplication.Data.Migrations
{
    public partial class FishingNet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Markers_Location_CoordinatesId",
                table: "Markers");

            migrationBuilder.DropForeignKey(
                name: "FK_Markers_InfoWindow_InfoId",
                table: "Markers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Location",
                table: "Location");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InfoWindow",
                table: "InfoWindow");

            migrationBuilder.RenameTable(
                name: "Location",
                newName: "Locations");

            migrationBuilder.RenameTable(
                name: "InfoWindow",
                newName: "InfoWindows");

            migrationBuilder.AddColumn<int>(
                name: "FishingNetId",
                table: "Markers",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Locations",
                table: "Locations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InfoWindows",
                table: "InfoWindows",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "FishingNets",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FishingNets", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Markers_FishingNetId",
                table: "Markers",
                column: "FishingNetId");

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

            migrationBuilder.DropTable(
                name: "FishingNets");

            migrationBuilder.DropIndex(
                name: "IX_Markers_FishingNetId",
                table: "Markers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Locations",
                table: "Locations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InfoWindows",
                table: "InfoWindows");

            migrationBuilder.DropColumn(
                name: "FishingNetId",
                table: "Markers");

            migrationBuilder.RenameTable(
                name: "Locations",
                newName: "Location");

            migrationBuilder.RenameTable(
                name: "InfoWindows",
                newName: "InfoWindow");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Location",
                table: "Location",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InfoWindow",
                table: "InfoWindow",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Markers_Location_CoordinatesId",
                table: "Markers",
                column: "CoordinatesId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Markers_InfoWindow_InfoId",
                table: "Markers",
                column: "InfoId",
                principalTable: "InfoWindow",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
