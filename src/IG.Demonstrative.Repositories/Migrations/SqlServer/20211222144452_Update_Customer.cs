using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrations.SqlServer
{
    public partial class Update_Customer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Customer_CustomerId",
                table: "Customer");

            migrationBuilder.DropIndex(
                name: "IX_Customer_CustomerId",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Customer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Customer",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customer_CustomerId",
                table: "Customer",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Customer_CustomerId",
                table: "Customer",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id");
        }
    }
}
