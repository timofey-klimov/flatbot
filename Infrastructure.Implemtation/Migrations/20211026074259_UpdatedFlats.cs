using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Implemtation.Migrations
{
    public partial class UpdatedFlats : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flats_Districts_DistrictId",
                table: "Flats");

            migrationBuilder.DropIndex(
                name: "IX_Flats_DistrictId",
                table: "Flats");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Flats");

            migrationBuilder.DropColumn(
                name: "Comission",
                table: "Flats");

            migrationBuilder.DropColumn(
                name: "DistrictId",
                table: "Flats");

            migrationBuilder.DropColumn(
                name: "FlatArea",
                table: "Flats");

            migrationBuilder.DropColumn(
                name: "Metro",
                table: "Flats");

            migrationBuilder.DropColumn(
                name: "Pledge",
                table: "Flats");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Flats");

            migrationBuilder.DropColumn(
                name: "TimeToMetro",
                table: "Flats");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "Flats");

            migrationBuilder.DropColumn(
                name: "WayToGo",
                table: "Flats");

            migrationBuilder.RenameColumn(
                name: "RoomCount",
                table: "Flats",
                newName: "RoomsCount");

            migrationBuilder.RenameColumn(
                name: "PdfReference",
                table: "Flats",
                newName: "LeaseTermType");

            migrationBuilder.RenameColumn(
                name: "MoreThanYear",
                table: "Flats",
                newName: "HasFurniture");

            migrationBuilder.RenameColumn(
                name: "MaxFloor",
                table: "Flats",
                newName: "FloorsCount");

            migrationBuilder.RenameColumn(
                name: "CianReference",
                table: "Flats",
                newName: "Description");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Flats",
                type: "datetime2(0)",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<double>(
                name: "CellingHeight",
                table: "Flats",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CianUrl",
                table: "Flats",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TotalArea",
                table: "Flats",
                type: "float",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlatId = table.Column<int>(type: "int", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OkrugId = table.Column<int>(type: "int", nullable: true),
                    Raion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    House = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Address_Districts_OkrugId",
                        column: x => x.OkrugId,
                        principalTable: "Districts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Address_Flats_FlatId",
                        column: x => x.FlatId,
                        principalTable: "Flats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BuildingInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlatId = table.Column<int>(type: "int", nullable: false),
                    BuildYear = table.Column<int>(type: "int", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuildingInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BuildingInfo_Flats_FlatId",
                        column: x => x.FlatId,
                        principalTable: "Flats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FlatGeo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlatId = table.Column<int>(type: "int", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlatGeo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlatGeo_Flats_FlatId",
                        column: x => x.FlatId,
                        principalTable: "Flats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ParkingInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlatId = table.Column<int>(type: "int", nullable: false),
                    ParkingType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsFree = table.Column<bool>(type: "bit", nullable: true),
                    PriceMonthly = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkingInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParkingInfo_Flats_FlatId",
                        column: x => x.FlatId,
                        principalTable: "Flats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Phone",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlatId = table.Column<int>(type: "int", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phone", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Phone_Flats_FlatId",
                        column: x => x.FlatId,
                        principalTable: "Flats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhotoInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlatId = table.Column<int>(type: "int", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotoInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhotoInfo_Flats_FlatId",
                        column: x => x.FlatId,
                        principalTable: "Flats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PriceInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlatId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Deposit = table.Column<int>(type: "int", nullable: true),
                    AgentFee = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PriceInfo_Flats_FlatId",
                        column: x => x.FlatId,
                        principalTable: "Flats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RailwayInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlatId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Time = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RailwayInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RailwayInfo_Flats_FlatId",
                        column: x => x.FlatId,
                        principalTable: "Flats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UndergroundInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlatId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Time = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UndergroundInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UndergroundInfo_Flats_FlatId",
                        column: x => x.FlatId,
                        principalTable: "Flats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Address_FlatId",
                table: "Address",
                column: "FlatId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Address_OkrugId",
                table: "Address",
                column: "OkrugId");

            migrationBuilder.CreateIndex(
                name: "IX_BuildingInfo_FlatId",
                table: "BuildingInfo",
                column: "FlatId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FlatGeo_FlatId",
                table: "FlatGeo",
                column: "FlatId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ParkingInfo_FlatId",
                table: "ParkingInfo",
                column: "FlatId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Phone_FlatId",
                table: "Phone",
                column: "FlatId");

            migrationBuilder.CreateIndex(
                name: "IX_PhotoInfo_FlatId",
                table: "PhotoInfo",
                column: "FlatId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceInfo_FlatId",
                table: "PriceInfo",
                column: "FlatId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RailwayInfo_FlatId",
                table: "RailwayInfo",
                column: "FlatId");

            migrationBuilder.CreateIndex(
                name: "IX_UndergroundInfo_FlatId",
                table: "UndergroundInfo",
                column: "FlatId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "BuildingInfo");

            migrationBuilder.DropTable(
                name: "FlatGeo");

            migrationBuilder.DropTable(
                name: "ParkingInfo");

            migrationBuilder.DropTable(
                name: "Phone");

            migrationBuilder.DropTable(
                name: "PhotoInfo");

            migrationBuilder.DropTable(
                name: "PriceInfo");

            migrationBuilder.DropTable(
                name: "RailwayInfo");

            migrationBuilder.DropTable(
                name: "UndergroundInfo");

            migrationBuilder.DropColumn(
                name: "CellingHeight",
                table: "Flats");

            migrationBuilder.DropColumn(
                name: "CianUrl",
                table: "Flats");

            migrationBuilder.DropColumn(
                name: "TotalArea",
                table: "Flats");

            migrationBuilder.RenameColumn(
                name: "RoomsCount",
                table: "Flats",
                newName: "RoomCount");

            migrationBuilder.RenameColumn(
                name: "LeaseTermType",
                table: "Flats",
                newName: "PdfReference");

            migrationBuilder.RenameColumn(
                name: "HasFurniture",
                table: "Flats",
                newName: "MoreThanYear");

            migrationBuilder.RenameColumn(
                name: "FloorsCount",
                table: "Flats",
                newName: "MaxFloor");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Flats",
                newName: "CianReference");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Flats",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Flats",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Comission",
                table: "Flats",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DistrictId",
                table: "Flats",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FlatArea",
                table: "Flats",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Metro",
                table: "Flats",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Pledge",
                table: "Flats",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Flats",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TimeToMetro",
                table: "Flats",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "Flats",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WayToGo",
                table: "Flats",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Flats_DistrictId",
                table: "Flats",
                column: "DistrictId");

            migrationBuilder.AddForeignKey(
                name: "FK_Flats_Districts_DistrictId",
                table: "Flats",
                column: "DistrictId",
                principalTable: "Districts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
