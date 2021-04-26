using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Budget_Tracker.Data.Migrations
{
    public partial class RemoveUserBudgets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserBudgets");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "UserBudgets",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationUsers = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Budgets = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBudgets", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserBudgets_AspNetUsers_ApplicationUsers",
                        column: x => x.ApplicationUsers,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserBudgets_Budgets_Budgets",
                        column: x => x.Budgets,
                        principalTable: "Budgets",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserBudgets_ApplicationUsers",
                table: "UserBudgets",
                column: "ApplicationUsers");

            migrationBuilder.CreateIndex(
                name: "IX_UserBudgets_Budgets",
                table: "UserBudgets",
                column: "Budgets");
        }
    }
}
