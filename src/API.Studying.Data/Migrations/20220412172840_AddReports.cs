using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Studying.Data.Migrations
{
    public partial class AddReports : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Views",
                table: "Card",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CardUserReport",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TopicId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Cards = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardUserReport", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CardUserReport_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CardUserReport_Topic_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topic",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CardViewReport",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CardId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardViewReport", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CardViewReport_Card_CardId",
                        column: x => x.CardId,
                        principalTable: "Card",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CardViewReport_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DocumentViewReport",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentViewReport", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentViewReport_Document_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Document",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DocumentViewReport_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExerciseUserReport",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TopicId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Exercises = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseUserReport", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExerciseUserReport_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExerciseUserReport_Topic_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topic",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExerciseViewReport",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExerciseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseViewReport", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExerciseViewReport_Exercise_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercise",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExerciseViewReport_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PostViewReport",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PostId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostViewReport", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostViewReport_Post_PostId",
                        column: x => x.PostId,
                        principalTable: "Post",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PostViewReport_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VideoViewReport",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    VideoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoViewReport", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VideoViewReport_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VideoViewReport_Video_VideoId",
                        column: x => x.VideoId,
                        principalTable: "Video",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CardUserReport_StudentId",
                table: "CardUserReport",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_CardUserReport_TopicId",
                table: "CardUserReport",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_CardViewReport_CardId",
                table: "CardViewReport",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_CardViewReport_StudentId",
                table: "CardViewReport",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentViewReport_DocumentId",
                table: "DocumentViewReport",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentViewReport_StudentId",
                table: "DocumentViewReport",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseUserReport_StudentId",
                table: "ExerciseUserReport",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseUserReport_TopicId",
                table: "ExerciseUserReport",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseViewReport_ExerciseId",
                table: "ExerciseViewReport",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseViewReport_StudentId",
                table: "ExerciseViewReport",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_PostViewReport_PostId",
                table: "PostViewReport",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostViewReport_StudentId",
                table: "PostViewReport",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_VideoViewReport_StudentId",
                table: "VideoViewReport",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_VideoViewReport_VideoId",
                table: "VideoViewReport",
                column: "VideoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CardUserReport");

            migrationBuilder.DropTable(
                name: "CardViewReport");

            migrationBuilder.DropTable(
                name: "DocumentViewReport");

            migrationBuilder.DropTable(
                name: "ExerciseUserReport");

            migrationBuilder.DropTable(
                name: "ExerciseViewReport");

            migrationBuilder.DropTable(
                name: "PostViewReport");

            migrationBuilder.DropTable(
                name: "VideoViewReport");

            migrationBuilder.DropColumn(
                name: "Views",
                table: "Card");
        }
    }
}
