using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Implemtation.Migrations
{
    public partial class FlatAreaandBuildingYear : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MinimumBuildingYear",
                table: "UserContext",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MinimumFlatArea",
                table: "UserContext",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MinimumBuildingYear",
                table: "UserContext");

            migrationBuilder.DropColumn(
                name: "MinimumFlatArea",
                table: "UserContext");
        }
    }
}
