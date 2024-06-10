using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class AddedAmountToExpensesVet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Pet_PetId",
                table: "Appointment");

            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "ExpensesVet",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "PetId",
                table: "Appointment",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_Pet_PetId",
                table: "Appointment",
                column: "PetId",
                principalTable: "Pet",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Pet_PetId",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "ExpensesVet");

            migrationBuilder.AlterColumn<int>(
                name: "PetId",
                table: "Appointment",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_Pet_PetId",
                table: "Appointment",
                column: "PetId",
                principalTable: "Pet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
