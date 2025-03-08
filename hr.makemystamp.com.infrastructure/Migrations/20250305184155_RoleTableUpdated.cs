using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hr.makemystamp.com.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RoleTableUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_RolesUsers_RoleId",
                table: "RolesUsers",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_RolesUsers_UserId",
                table: "RolesUsers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_RolesUsers_Roles_RoleId",
                table: "RolesUsers",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RolesUsers_Users_UserId",
                table: "RolesUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RolesUsers_Roles_RoleId",
                table: "RolesUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_RolesUsers_Users_UserId",
                table: "RolesUsers");

            migrationBuilder.DropIndex(
                name: "IX_RolesUsers_RoleId",
                table: "RolesUsers");

            migrationBuilder.DropIndex(
                name: "IX_RolesUsers_UserId",
                table: "RolesUsers");
        }
    }
}
