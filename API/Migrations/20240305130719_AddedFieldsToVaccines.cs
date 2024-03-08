using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class AddedFieldsToVaccines : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Vaccines",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Required",
                table: "Vaccines",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SideEffects",
                table: "Vaccines",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Vaccines");

            migrationBuilder.DropColumn(
                name: "Required",
                table: "Vaccines");

            migrationBuilder.DropColumn(
                name: "SideEffects",
                table: "Vaccines");
        }
    }
}
