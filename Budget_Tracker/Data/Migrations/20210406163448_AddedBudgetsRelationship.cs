using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Budget_Tracker.Data.Migrations
{
    public partial class AddedBudgetsRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BudgetID",
                table: "BudgetCategories",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_BudgetCategories_BudgetID",
                table: "BudgetCategories",
                column: "BudgetID");

            migrationBuilder.AddForeignKey(
                name: "FK_BudgetCategories_Budgets_BudgetID",
                table: "BudgetCategories",
                column: "BudgetID",
                principalTable: "Budgets",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BudgetCategories_Budgets_BudgetID",
                table: "BudgetCategories");

            migrationBuilder.DropIndex(
                name: "IX_BudgetCategories_BudgetID",
                table: "BudgetCategories");

            migrationBuilder.DropColumn(
                name: "BudgetID",
                table: "BudgetCategories");
        }
    }
}
