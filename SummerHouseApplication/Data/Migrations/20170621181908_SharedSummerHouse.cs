using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SummerHouseApplication.Data.Migrations
{
    public partial class SharedSummerHouse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SharedSummerHouses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SummerHouseId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    UserId1 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SharedSummerHouses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SharedSummerHouses_SummerHouses_SummerHouseId",
                        column: x => x.SummerHouseId,
                        principalTable: "SummerHouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SharedSummerHouses_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SharedSummerHouses_SummerHouseId",
                table: "SharedSummerHouses",
                column: "SummerHouseId");

            migrationBuilder.CreateIndex(
                name: "IX_SharedSummerHouses_UserId1",
                table: "SharedSummerHouses",
                column: "UserId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SharedSummerHouses");
        }
    }
}
