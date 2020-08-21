using Microsoft.EntityFrameworkCore.Migrations;

namespace go_live.Migrations
{
    public partial class AddDbSetEntry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectOwner_Projects_ProjectId",
                table: "ProjectOwner");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectOwner_Users_UserId",
                table: "ProjectOwner");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectOwner",
                table: "ProjectOwner");

            migrationBuilder.RenameTable(
                name: "ProjectOwner",
                newName: "ProjectOwners");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectOwner_UserId",
                table: "ProjectOwners",
                newName: "IX_ProjectOwners_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectOwners",
                table: "ProjectOwners",
                columns: new[] { "ProjectId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectOwners_Projects_ProjectId",
                table: "ProjectOwners",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectOwners_Users_UserId",
                table: "ProjectOwners",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectOwners_Projects_ProjectId",
                table: "ProjectOwners");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectOwners_Users_UserId",
                table: "ProjectOwners");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectOwners",
                table: "ProjectOwners");

            migrationBuilder.RenameTable(
                name: "ProjectOwners",
                newName: "ProjectOwner");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectOwners_UserId",
                table: "ProjectOwner",
                newName: "IX_ProjectOwner_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectOwner",
                table: "ProjectOwner",
                columns: new[] { "ProjectId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectOwner_Projects_ProjectId",
                table: "ProjectOwner",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectOwner_Users_UserId",
                table: "ProjectOwner",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
