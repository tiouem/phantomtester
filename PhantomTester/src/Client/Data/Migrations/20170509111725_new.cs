using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Client.Data.Migrations
{
    public partial class @new : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tokens_User_UserId",
                table: "Tokens");

            migrationBuilder.DropIndex(
                name: "IX_Tokens_UserId",
                table: "Tokens");

            migrationBuilder.AddColumn<int>(
                name: "Usages",
                table: "Tokens",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_User_TokenId",
                table: "User",
                column: "TokenId",
                unique: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Tokens",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Tokens_TokenId",
                table: "User",
                column: "TokenId",
                principalTable: "Tokens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Tokens_TokenId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_TokenId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Usages",
                table: "Tokens");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Tokens",
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "IX_Tokens_UserId",
                table: "Tokens",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tokens_User_UserId",
                table: "Tokens",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
