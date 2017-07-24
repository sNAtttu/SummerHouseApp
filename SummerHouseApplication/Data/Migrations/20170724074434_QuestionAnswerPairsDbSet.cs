using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SummerHouseApplication.Data.Migrations
{
    public partial class QuestionAnswerPairsDbSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionAnswer_SummerHouses_SummerHouseId",
                table: "QuestionAnswer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuestionAnswer",
                table: "QuestionAnswer");

            migrationBuilder.RenameTable(
                name: "QuestionAnswer",
                newName: "QuestionAnswers");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionAnswer_SummerHouseId",
                table: "QuestionAnswers",
                newName: "IX_QuestionAnswers_SummerHouseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuestionAnswers",
                table: "QuestionAnswers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionAnswers_SummerHouses_SummerHouseId",
                table: "QuestionAnswers",
                column: "SummerHouseId",
                principalTable: "SummerHouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionAnswers_SummerHouses_SummerHouseId",
                table: "QuestionAnswers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuestionAnswers",
                table: "QuestionAnswers");

            migrationBuilder.RenameTable(
                name: "QuestionAnswers",
                newName: "QuestionAnswer");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionAnswers_SummerHouseId",
                table: "QuestionAnswer",
                newName: "IX_QuestionAnswer_SummerHouseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuestionAnswer",
                table: "QuestionAnswer",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionAnswer_SummerHouses_SummerHouseId",
                table: "QuestionAnswer",
                column: "SummerHouseId",
                principalTable: "SummerHouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
