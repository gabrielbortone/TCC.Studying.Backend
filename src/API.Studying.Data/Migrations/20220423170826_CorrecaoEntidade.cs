using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Studying.Data.Migrations
{
    public partial class CorrecaoEntidade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CardId",
                table: "DocumentViewReport",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StudentId1",
                table: "DocumentViewReport",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DocumentViewReport_CardId",
                table: "DocumentViewReport",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentViewReport_StudentId1",
                table: "DocumentViewReport",
                column: "StudentId1");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentViewReport_Card_CardId",
                table: "DocumentViewReport",
                column: "CardId",
                principalTable: "Card",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentViewReport_Student_StudentId1",
                table: "DocumentViewReport",
                column: "StudentId1",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentViewReport_Card_CardId",
                table: "DocumentViewReport");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentViewReport_Student_StudentId1",
                table: "DocumentViewReport");

            migrationBuilder.DropIndex(
                name: "IX_DocumentViewReport_CardId",
                table: "DocumentViewReport");

            migrationBuilder.DropIndex(
                name: "IX_DocumentViewReport_StudentId1",
                table: "DocumentViewReport");

            migrationBuilder.DropColumn(
                name: "CardId",
                table: "DocumentViewReport");

            migrationBuilder.DropColumn(
                name: "StudentId1",
                table: "DocumentViewReport");
        }
    }
}
