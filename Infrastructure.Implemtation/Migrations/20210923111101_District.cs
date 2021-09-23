using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Implemtation.Migrations
{
    public partial class District : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Metro");

            migrationBuilder.CreateTable(
                name: "Disctrict",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disctrict", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DisctrictUserContext",
                columns: table => new
                {
                    DisctrictId = table.Column<int>(type: "int", nullable: false),
                    UserContextId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisctrictUserContext", x => new { x.DisctrictId, x.UserContextId });
                    table.ForeignKey(
                        name: "FK_DisctrictUserContext_Disctrict_DisctrictId",
                        column: x => x.DisctrictId,
                        principalTable: "Disctrict",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DisctrictUserContext_UserContext_UserContextId",
                        column: x => x.UserContextId,
                        principalTable: "UserContext",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Disctrict",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "ЦАО" },
                    { 2, "СВАО" },
                    { 3, "СЗАО" },
                    { 4, "ЮАО" },
                    { 5, "ЗАО" },
                    { 6, "ЮВАО" },
                    { 7, "ЮЗАО" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DisctrictUserContext_UserContextId",
                table: "DisctrictUserContext",
                column: "UserContextId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DisctrictUserContext");

            migrationBuilder.DropTable(
                name: "Disctrict");

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
