using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class UserToAdoptions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Adoption",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Adoption_AppUserId",
                table: "Adoption",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Adoption_AspNetUsers_AppUserId",
                table: "Adoption",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adoption_AspNetUsers_AppUserId",
                table: "Adoption");

            migrationBuilder.DropIndex(
                name: "IX_Adoption_AppUserId",
                table: "Adoption");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Adoption");
        }
    }
}
