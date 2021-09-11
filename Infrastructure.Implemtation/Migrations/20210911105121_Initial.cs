using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Implemtation.Migrations
{
    public partial class Initial : Migration
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
                    FlatArea = table.Column<double>(type: "float", nullable: false),
                    CurrentFloor = table.Column<int>(type: "int", nullable: false),
                    MaxFloor = table.Column<int>(type: "int", nullable: false),
                    Metro = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TimeToMetro = table.Column<int>(type: "int", nullable: true),
                    WayToGo = table.Column<int>(type: "int", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    NeedComission = table.Column<bool>(type: "bit", nullable: true),
                    NeedPledge = table.Column<bool>(type: "bit", nullable: true),
                    Comission = table.Column<int>(type: "int", nullable: true),
                    Pledge = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MoreThanYear = table.Column<bool>(type: "bit", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CianReference = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
