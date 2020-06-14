using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskManager.Data.Migrations
{
    public partial class updates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Completed",
                table: "UserTasks");

            migrationBuilder.DropColumn(
                name: "Taken",
                table: "UserTasks");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "UserTasks",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "UserTasks");

            migrationBuilder.AddColumn<bool>(
                name: "Completed",
                table: "UserTasks",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Taken",
                table: "UserTasks",
                nullable: false,
                defaultValue: false);
        }
    }
}
