using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class RemovedPetAdoptions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adoption_Pet_PetId",
                table: "Adoption");

            migrationBuilder.DropIndex(
                name: "IX_Adoption_PetId",
                table: "Adoption");

            migrationBuilder.DropColumn(
                name: "PetId",
                table: "Adoption");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "Adoption",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Adoption",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Adoption");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Adoption");

            migrationBuilder.AddColumn<int>(
                name: "PetId",
                table: "Adoption",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Adoption_PetId",
                table: "Adoption",
                column: "PetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Adoption_Pet_PetId",
                table: "Adoption",
                column: "PetId",
                principalTable: "Pet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
