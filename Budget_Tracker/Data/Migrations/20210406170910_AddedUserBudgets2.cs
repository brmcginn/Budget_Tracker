using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Budget_Tracker.Data.Migrations
{
    public partial class AddedUserBudgets2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserBudgets",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Budgets = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ApplicationUsers = table.Column<string>(type: "nvarchar(450)", nullable: true)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserBudgets");
        }
    }
}
