using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HabitsApp.Server.Migrations
{
    /// <inheritdoc />
    public partial class added_goalId_attribute_to_calendarEntry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GoalId",
                table: "CalendarEntries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "CalendarEntries",
                keyColumn: "Id",
                keyValue: 1,
                column: "GoalId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "CalendarEntries",
                keyColumn: "Id",
                keyValue: 2,
                column: "GoalId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "CalendarEntries",
                keyColumn: "Id",
                keyValue: 3,
                column: "GoalId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "CalendarEntries",
                keyColumn: "Id",
                keyValue: 4,
                column: "GoalId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "CalendarEntries",
                keyColumn: "Id",
                keyValue: 5,
                column: "GoalId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "CalendarEntries",
                keyColumn: "Id",
                keyValue: 6,
                column: "GoalId",
                value: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GoalId",
                table: "CalendarEntries");
        }
    }
}
