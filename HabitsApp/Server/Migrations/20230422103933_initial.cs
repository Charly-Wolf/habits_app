using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HabitsApp.Server.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Activities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Activities_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CalendarEntries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActivityId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Start = table.Column<DateTime>(type: "datetime2", nullable: false),
                    End = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalendarEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CalendarEntries_Activities_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Goals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActivityId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DurationMinutes = table.Column<int>(type: "int", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Goals_Activities_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Sport" },
                    { 2, "Hobby" },
                    { 3, "Coding" },
                    { 4, "Ausbildung" },
                    { 5, "Language" },
                    { 6, "Youtube" },
                    { 7, "Health" }
                });

            migrationBuilder.InsertData(
                table: "Activities",
                columns: new[] { "Id", "CategoryId", "Name" },
                values: new object[,]
                {
                    { 1, 2, "Guitar" },
                    { 2, 3, "Blazor" },
                    { 3, 7, "Meditation" },
                    { 4, 4, "LF8: Project - Data Serialization" },
                    { 5, 5, "Italian" },
                    { 6, 1, "Freeletics" },
                    { 7, 1, "Running" },
                    { 8, 6, "Video 3" },
                    { 9, 2, "Analog Photography" },
                    { 10, 2, "Digital Photography" },
                    { 11, 7, "Cook new healthy recipy" },
                    { 12, 3, "React" },
                    { 13, 5, "German" }
                });

            migrationBuilder.InsertData(
                table: "CalendarEntries",
                columns: new[] { "Id", "ActivityId", "Comment", "Date", "End", "Start" },
                values: new object[,]
                {
                    { 1, 3, null, new DateTime(2023, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 4, 12, 6, 20, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 4, 12, 6, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 7, "First time running after a long time", new DateTime(2023, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 4, 12, 6, 55, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 4, 12, 6, 30, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 1, null, new DateTime(2023, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 4, 12, 17, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 4, 12, 17, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 1, null, new DateTime(2023, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 4, 12, 17, 50, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 4, 12, 17, 30, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 5, null, new DateTime(2023, 4, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 4, 13, 17, 50, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 4, 13, 17, 30, 0, 0, DateTimeKind.Unspecified) },
                    { 6, 2, "Started planning app (DB Design)", new DateTime(2023, 4, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 4, 13, 21, 15, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 4, 13, 20, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Goals",
                columns: new[] { "Id", "ActivityId", "Date", "DurationMinutes", "IsCompleted" },
                values: new object[,]
                {
                    { 1, 3, new DateTime(2023, 4, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 15, false },
                    { 2, 1, new DateTime(2023, 4, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 45, false },
                    { 3, 5, new DateTime(2023, 4, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 30, false },
                    { 4, 7, new DateTime(2023, 4, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 30, false },
                    { 5, 6, new DateTime(2023, 4, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 40, false },
                    { 6, 3, new DateTime(2023, 4, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 15, false },
                    { 7, 1, new DateTime(2023, 4, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 30, false },
                    { 8, 2, new DateTime(2023, 4, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 660, true }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activities_CategoryId",
                table: "Activities",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CalendarEntries_ActivityId",
                table: "CalendarEntries",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_Goals_ActivityId",
                table: "Goals",
                column: "ActivityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CalendarEntries");

            migrationBuilder.DropTable(
                name: "Goals");

            migrationBuilder.DropTable(
                name: "Activities");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
