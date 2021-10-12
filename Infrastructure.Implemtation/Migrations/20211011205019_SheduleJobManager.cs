using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Implemtation.Migrations
{
    public partial class SheduleJobManager : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "JobHistory");

            migrationBuilder.DropColumn(
                name: "NextFireAt",
                table: "JobHistory");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "JobHistory");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "JobHistory",
                newName: "FinishTime");

            migrationBuilder.AlterColumn<string>(
                name: "Message",
                table: "JobHistory",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SheduleJobManagerId",
                table: "JobHistory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "SheduleJobManagers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobManagerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlanningRunTime = table.Column<DateTime>(type: "datetime2(0)", nullable: true),
                    RunTime = table.Column<DateTime>(type: "datetime2(0)", nullable: true),
                    Period = table.Column<int>(type: "int", nullable: false),
                    OrderNumber = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SheduleJobManagers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobHistory_SheduleJobManagerId",
                table: "JobHistory",
                column: "SheduleJobManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobHistory_SheduleJobManagers_SheduleJobManagerId",
                table: "JobHistory",
                column: "SheduleJobManagerId",
                principalTable: "SheduleJobManagers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobHistory_SheduleJobManagers_SheduleJobManagerId",
                table: "JobHistory");

            migrationBuilder.DropTable(
                name: "SheduleJobManagers");

            migrationBuilder.DropIndex(
                name: "IX_JobHistory_SheduleJobManagerId",
                table: "JobHistory");

            migrationBuilder.DropColumn(
                name: "SheduleJobManagerId",
                table: "JobHistory");

            migrationBuilder.RenameColumn(
                name: "FinishTime",
                table: "JobHistory",
                newName: "EndDate");

            migrationBuilder.AlterColumn<string>(
                name: "Message",
                table: "JobHistory",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "JobHistory",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NextFireAt",
                table: "JobHistory",
                type: "datetime2(2)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "JobHistory",
                type: "datetime2(0)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
