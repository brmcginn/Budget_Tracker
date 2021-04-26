using Microsoft.EntityFrameworkCore.Migrations;

namespace Budget_Tracker.Data.Migrations
{
    public partial class ChangeBudgetUserToString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Budgets_AspNetUsers_ApplicationUsers",
                table: "Budgets");

            migrationBuilder.DropIndex(
                name: "IX_Budgets_ApplicationUsers",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "ApplicationUsers",
                table: "Budgets");

            migrationBuilder.AddColumn<string>(
                name: "User",
                table: "Budgets",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "User",
                table: "Budgets");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUsers",
                table: "Budgets",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_ApplicationUsers",
                table: "Budgets",
                column: "ApplicationUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_Budgets_AspNetUsers_ApplicationUsers",
                table: "Budgets",
                column: "ApplicationUsers",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
