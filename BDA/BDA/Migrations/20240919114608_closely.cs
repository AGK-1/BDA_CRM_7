using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BDA.Migrations
{
    /// <inheritdoc />
    public partial class closely : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Users_CreatedByUserId",
                table: "Customer");

            migrationBuilder.AlterColumn<int>(
                name: "CreatedByUserId",
                table: "Customer",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Users_CreatedByUserId",
                table: "Customer",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Users_CreatedByUserId",
                table: "Customer");

            migrationBuilder.AlterColumn<int>(
                name: "CreatedByUserId",
                table: "Customer",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Users_CreatedByUserId",
                table: "Customer",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
