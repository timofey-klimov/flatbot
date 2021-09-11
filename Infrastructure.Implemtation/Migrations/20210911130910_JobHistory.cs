using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Implemtation.Migrations
{
    public partial class JobHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NeedComission",
                table: "Flats");

            migrationBuilder.DropColumn(
                name: "NeedPledge",
                table: "Flats");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "Flats",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "Flats",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "JobHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2(0)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobHistory", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobHistory");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "Flats");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "Flats");

            migrationBuilder.AddColumn<bool>(
                name: "NeedComission",
                table: "Flats",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "NeedPledge",
                table: "Flats",
                type: "bit",
                nullable: true);
        }
    }
}
