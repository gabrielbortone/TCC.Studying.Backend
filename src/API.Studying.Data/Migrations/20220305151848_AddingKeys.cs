using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Studying.Data.Migrations
{
    public partial class AddingKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Keys",
                table: "Video",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataPublished",
                table: "Post",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Keys",
                table: "Post",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Keys",
                table: "Playlist",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Keys",
                table: "Exercise",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Keys",
                table: "Document",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Keys",
                table: "DeckOfCards",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "StarComment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CommentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StarComment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StarDeckOfCard",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeckOfCardsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StarDeckOfCard", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StarDocument",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StarDocument", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StarExercise",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExerciseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlaylistId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StarPlaylist", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StarPost",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StarPost", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StarTopic",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TopicId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StarTopic", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StarVideo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VideoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StarVideo", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StarComment");

            migrationBuilder.DropTable(
                name: "StarDeckOfCard");

            migrationBuilder.DropTable(
                name: "StarDocument");

            migrationBuilder.DropTable(
                name: "StarExercise");

            migrationBuilder.DropTable(
                name: "StarPlaylist");

            migrationBuilder.DropTable(
                name: "StarPost");

            migrationBuilder.DropTable(
                name: "StarTopic");

            migrationBuilder.DropTable(
                name: "StarVideo");

            migrationBuilder.DropColumn(
                name: "Keys",
                table: "Video");

            migrationBuilder.DropColumn(
                name: "DataPublished",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "Keys",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "Keys",
                table: "Playlist");

            migrationBuilder.DropColumn(
                name: "Keys",
                table: "Exercise");

            migrationBuilder.DropColumn(
                name: "Keys",
                table: "Document");

            migrationBuilder.DropColumn(
                name: "Keys",
                table: "DeckOfCards");
        }
    }
}
