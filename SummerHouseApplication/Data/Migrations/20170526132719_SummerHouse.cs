using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SummerHouseApplication.Data.Migrations
{
    public partial class SummerHouse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SummerHouseId",
                table: "Markers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SummerHouseId",
                table: "FishingNets",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SummerHouses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    HasBeach = table.Column<bool>(nullable: false),
                    HasElectricity = table.Column<bool>(nullable: false),
                    HasRunningWater = table.Column<bool>(nullable: false),
                    HasSauna = table.Column<bool>(nullable: false),
                    LocationOnMapId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    OwnerId = table.Column<string>(nullable: true),
                    PostalCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SummerHouses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SummerHouses_Locations_LocationOnMapId",
                        column: x => x.LocationOnMapId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SummerHouses_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Markers_SummerHouseId",
                table: "Markers",
                column: "SummerHouseId");

            migrationBuilder.CreateIndex(
                name: "IX_FishingNets_SummerHouseId",
                table: "FishingNets",
                column: "SummerHouseId");

            migrationBuilder.CreateIndex(
                name: "IX_SummerHouses_LocationOnMapId",
                table: "SummerHouses",
                column: "LocationOnMapId");

            migrationBuilder.CreateIndex(
                name: "IX_SummerHouses_OwnerId",
                table: "SummerHouses",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_FishingNets_SummerHouses_SummerHouseId",
                table: "FishingNets",
                column: "SummerHouseId",
                principalTable: "SummerHouses",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FishingNets_SummerHouses_SummerHouseId",
                table: "FishingNets");

            migrationBuilder.DropForeignKey(
                name: "FK_Markers_SummerHouses_SummerHouseId",
                table: "Markers");

            migrationBuilder.DropTable(
                name: "SummerHouses");

            migrationBuilder.DropIndex(
                name: "IX_Markers_SummerHouseId",
                table: "Markers");

            migrationBuilder.DropIndex(
                name: "IX_FishingNets_SummerHouseId",
                table: "FishingNets");

            migrationBuilder.DropColumn(
                name: "SummerHouseId",
                table: "Markers");

            migrationBuilder.DropColumn(
                name: "SummerHouseId",
                table: "FishingNets");
        }
    }
}
