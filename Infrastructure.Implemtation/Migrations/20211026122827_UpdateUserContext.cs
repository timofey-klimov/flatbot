using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Implemtation.Migrations
{
    public partial class UpdateUserContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DistrictUserContext_Districts_DisctrictsId",
                table: "DistrictUserContext");

            migrationBuilder.DropColumn(
                name: "PostedNotifications",
                table: "UserContext");

            migrationBuilder.DropColumn(
                name: "RoomCountContext",
                table: "UserContext");

            migrationBuilder.RenameColumn(
                name: "DisctrictsId",
                table: "DistrictUserContext",
                newName: "DistrictsId");

            migrationBuilder.CreateTable(
                name: "PostedNotification",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserContextId = table.Column<int>(type: "int", nullable: false),
                    CianId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostedNotification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostedNotification_UserContext_UserContextId",
                        column: x => x.UserContextId,
                        principalTable: "UserContext",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoomCount",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserContextId = table.Column<int>(type: "int", nullable: false),
                    RoomCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoomCount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRoomCount_UserContext_UserContextId",
                        column: x => x.UserContextId,
                        principalTable: "UserContext",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostedNotification_UserContextId",
                table: "PostedNotification",
                column: "UserContextId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoomCount_UserContextId",
                table: "UserRoomCount",
                column: "UserContextId");

            migrationBuilder.AddForeignKey(
                name: "FK_DistrictUserContext_Districts_DistrictsId",
                table: "DistrictUserContext",
                column: "DistrictsId",
                principalTable: "Districts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DistrictUserContext_Districts_DistrictsId",
                table: "DistrictUserContext");

            migrationBuilder.DropTable(
                name: "PostedNotification");

            migrationBuilder.DropTable(
                name: "UserRoomCount");

            migrationBuilder.RenameColumn(
                name: "DistrictsId",
                table: "DistrictUserContext",
                newName: "DisctrictsId");

            migrationBuilder.AddColumn<string>(
                name: "PostedNotifications",
                table: "UserContext",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RoomCountContext",
                table: "UserContext",
                type: "int",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DistrictUserContext_Districts_DisctrictsId",
                table: "DistrictUserContext",
                column: "DisctrictsId",
                principalTable: "Districts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
