using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueConstraintToVetServices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_VetServices_VetId",
                table: "VetServices");

            migrationBuilder.InsertData(
                table: "Service",
                columns: new[] { "Id", "Description", "Duration", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Description 1", 60, "Service 1", 100f },
                    { 2, "Description 2", 120, "Service 2", 200f },
                    { 3, "Description 3", 180, "Service 3", 300f }
                });

            migrationBuilder.CreateIndex(
                name: "IX_VetServices_VetId_ServiceId",
                table: "VetServices",
                columns: new[] { "VetId", "ServiceId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_VetServices_VetId_ServiceId",
                table: "VetServices");

            migrationBuilder.DeleteData(
                table: "Service",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Service",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Service",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.CreateIndex(
                name: "IX_VetServices_VetId",
                table: "VetServices",
                column: "VetId");
        }
    }
}
