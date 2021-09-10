using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Implemtation.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Flats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CianId = table.Column<long>(type: "bigint", nullable: false),
                    RoomCount = table.Column<int>(type: "int", nullable: false),
                    MetroName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TimeToGo = table.Column<int>(type: "int", nullable: true),
                    OnCar = table.Column<bool>(type: "bit", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RoomArea = table.Column<double>(type: "float", nullable: false),
                    CurrentFloor = table.Column<int>(type: "int", nullable: false),
                    LastFloor = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CeilingHeight = table.Column<double>(type: "float", nullable: true),
                    Reference = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flats", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Flats");
        }
    }
}
