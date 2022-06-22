using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Studying.Data.Migrations
{
    public partial class MigracaoDaDiminuicao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Card_Card_NextId",
                table: "Card");

            migrationBuilder.DropForeignKey(
                name: "FK_Card_DeckOfCards_DeckOfCardsId",
                table: "Card");

            migrationBuilder.DropForeignKey(
                name: "FK_Video_Playlist_PlaylistId",
                table: "Video");

            migrationBuilder.DropTable(
                name: "Commitment");

            migrationBuilder.DropTable(
                name: "DeckOfCards");

            migrationBuilder.DropTable(
                name: "Playlist");

            migrationBuilder.DropTable(
                name: "StarDeckOfCard");

            migrationBuilder.DropTable(
                name: "StarExercise");

            migrationBuilder.DropTable(
                name: "StarPlaylist");

            migrationBuilder.DropTable(
                name: "StudentDeckOfCards");

            migrationBuilder.DropTable(
                name: "StudentPlaylist");

            migrationBuilder.DropIndex(
                name: "IX_Card_NextId",
                table: "Card");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Exercise");

            migrationBuilder.RenameColumn(
                name: "PlaylistId",
                table: "Video",
                newName: "StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_Video_PlaylistId",
                table: "Video",
                newName: "IX_Video_StudentId");

            migrationBuilder.RenameColumn(
                name: "NextId",
                table: "Card",
                newName: "TopicId");

            migrationBuilder.RenameColumn(
                name: "DeckOfCardsId",
                table: "Card",
                newName: "StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_Card_DeckOfCardsId",
                table: "Card",
                newName: "IX_Card_StudentId");

            migrationBuilder.AlterColumn<string>(
                name: "UrlVideo",
                table: "Video",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Video",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Thumbnail",
                table: "Video",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Keys",
                table: "Video",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Keys",
                table: "Card",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Card_TopicId",
                table: "Card",
                column: "TopicId");

            migrationBuilder.AddForeignKey(
                name: "FK_Card_Student_StudentId",
                table: "Card",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Card_Topic_TopicId",
                table: "Card",
                column: "TopicId",
                principalTable: "Topic",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Video_Student_StudentId",
                table: "Video",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Card_Student_StudentId",
                table: "Card");

            migrationBuilder.DropForeignKey(
                name: "FK_Card_Topic_TopicId",
                table: "Card");

            migrationBuilder.DropForeignKey(
                name: "FK_Video_Student_StudentId",
                table: "Video");

            migrationBuilder.DropIndex(
                name: "IX_Card_TopicId",
                table: "Card");

            migrationBuilder.DropColumn(
                name: "Keys",
                table: "Card");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "Video",
                newName: "PlaylistId");

            migrationBuilder.RenameIndex(
                name: "IX_Video_StudentId",
                table: "Video",
                newName: "IX_Video_PlaylistId");

            migrationBuilder.RenameColumn(
                name: "TopicId",
                table: "Card",
                newName: "NextId");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "Card",
                newName: "DeckOfCardsId");

            migrationBuilder.RenameIndex(
                name: "IX_Card_StudentId",
                table: "Card",
                newName: "IX_Card_DeckOfCardsId");

            migrationBuilder.AlterColumn<string>(
                name: "UrlVideo",
                table: "Video",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Video",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Thumbnail",
                table: "Video",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Keys",
                table: "Video",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Exercise",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Commitment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsFinished = table.Column<bool>(type: "bit", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(125)", maxLength: 125, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commitment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Commitment_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DeckOfCards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Keys = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Stars = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(125)", maxLength: 125, nullable: true),
                    TopicId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Views = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeckOfCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeckOfCards_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeckOfCards_Topic_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topic",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Playlist",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(125)", maxLength: 125, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Keys = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Stars = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Thumbnail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(125)", maxLength: 125, nullable: true),
                    TopicId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Views = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Playlist", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Playlist_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Playlist_Topic_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topic",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StarDeckOfCard",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeckOfCardsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StarDeckOfCard", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StarExercise",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExerciseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StarExercise", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StarPlaylist",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlaylistId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StarPlaylist", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudentDeckOfCards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeckOfCardsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentDeckOfCards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudentPlaylist",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlaylistId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentPlaylist", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Card_NextId",
                table: "Card",
                column: "NextId",
                unique: true,
                filter: "[NextId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Commitment_StudentId",
                table: "Commitment",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_DeckOfCards_StudentId",
                table: "DeckOfCards",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_DeckOfCards_TopicId",
                table: "DeckOfCards",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_Playlist_StudentId",
                table: "Playlist",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Playlist_TopicId",
                table: "Playlist",
                column: "TopicId");

            migrationBuilder.AddForeignKey(
                name: "FK_Card_Card_NextId",
                table: "Card",
                column: "NextId",
                principalTable: "Card",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Card_DeckOfCards_DeckOfCardsId",
                table: "Card",
                column: "DeckOfCardsId",
                principalTable: "DeckOfCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Video_Playlist_PlaylistId",
                table: "Video",
                column: "PlaylistId",
                principalTable: "Playlist",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
