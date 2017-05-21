using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Client.Data.Migrations
{
    public partial class ForeignKeyUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Tokens_TokenId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_TokenId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Tokens",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tokens_UserId",
                table: "Tokens",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tokens_AspNetUsers_UserId",
                table: "Tokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tokens_AspNetUsers_UserId",
                table: "Tokens");

            migrationBuilder.DropIndex(
                name: "IX_Tokens_UserId",
                table: "Tokens");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Tokens",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_TokenId",
                table: "AspNetUsers",
                column: "TokenId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Tokens_TokenId",
                table: "AspNetUsers",
                column: "TokenId",
                principalTable: "Tokens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
