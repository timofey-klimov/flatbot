using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Implemtation.Migrations
{
    public partial class TimedJobManager : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "JobType",
                table: "SheduleJobManagers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "SheduleRunTime",
                table: "SheduleJobManagers",
                type: "datetime2(0)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JobType",
                table: "SheduleJobManagers");

            migrationBuilder.DropColumn(
                name: "SheduleRunTime",
                table: "SheduleJobManagers");
        }
    }
}
