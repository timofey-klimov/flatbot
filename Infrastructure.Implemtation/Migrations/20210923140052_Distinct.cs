using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Implemtation.Migrations
{
    public partial class Distinct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Metro");

            migrationBuilder.AddColumn<int>(
                name: "DistrictId",
                table: "Flats",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Districts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Districts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DistrictUserContext",
                columns: table => new
                {
                    DisctrictsId = table.Column<int>(type: "int", nullable: false),
                    UserContextsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistrictUserContext", x => new { x.DisctrictsId, x.UserContextsId });
                    table.ForeignKey(
                        name: "FK_DistrictUserContext_Districts_DisctrictsId",
                        column: x => x.DisctrictsId,
                        principalTable: "Districts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DistrictUserContext_UserContext_UserContextsId",
                        column: x => x.UserContextsId,
                        principalTable: "UserContext",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Districts",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "ЦАО" },
                    { 2, "СВАО" },
                    { 3, "СЗАО" },
                    { 4, "ЮАО" },
                    { 5, "ЗАО" },
                    { 6, "САО" },
                    { 7, "ВАО" },
                    { 8, "ЮВАО" },
                    { 9, "ЮЗАО" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Flats_DistrictId",
                table: "Flats",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_DistrictUserContext_UserContextsId",
                table: "DistrictUserContext",
                column: "UserContextsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Flats_Districts_DistrictId",
                table: "Flats",
                column: "DistrictId",
                principalTable: "Districts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flats_Districts_DistrictId",
                table: "Flats");

            migrationBuilder.DropTable(
                name: "DistrictUserContext");

            migrationBuilder.DropTable(
                name: "Districts");

            migrationBuilder.DropIndex(
                name: "IX_Flats_DistrictId",
                table: "Flats");

            migrationBuilder.DropColumn(
                name: "DistrictId",
                table: "Flats");

            migrationBuilder.CreateTable(
                name: "Metro",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserContextId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Metro", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Metro_UserContext_UserContextId",
                        column: x => x.UserContextId,
                        principalTable: "UserContext",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Metro_UserContextId",
                table: "Metro",
                column: "UserContextId");
        }
    }
}
