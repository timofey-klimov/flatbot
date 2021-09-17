using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Implemtation.Migrations
{
    public partial class UserContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChatId = table.Column<long>(type: "bigint", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserContext",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoomCountContext = table.Column<int>(type: "int", nullable: true),
                    MinimumPrice = table.Column<int>(type: "int", nullable: true),
                    MaximumPrice = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserContext", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserContext_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_UserContext_UserId",
                table: "UserContext",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Metro");

            migrationBuilder.DropTable(
                name: "UserContext");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
