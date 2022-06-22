using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Studying.Data.Migrations
{
    public partial class AtualizacaoStars : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "StudentId",
                table: "Post",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Stars",
                table: "DeckOfCards",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Post_StudentId",
                table: "Post",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Post_Student_StudentId",
                table: "Post",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Post_Student_StudentId",
                table: "Post");

            migrationBuilder.DropIndex(
                name: "IX_Post_StudentId",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "Stars",
                table: "DeckOfCards");
        }
    }
}
