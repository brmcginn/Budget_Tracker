using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Budget_Tracker.Data.Migrations
{
    public partial class AddedCategoryRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BudgetCategoryID",
                table: "Expenses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_BudgetCategoryID",
                table: "Expenses",
                column: "BudgetCategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_BudgetCategories_BudgetCategoryID",
                table: "Expenses",
                column: "BudgetCategoryID",
                principalTable: "BudgetCategories",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_BudgetCategories_BudgetCategoryID",
                table: "Expenses");

            migrationBuilder.DropIndex(
                name: "IX_Expenses_BudgetCategoryID",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "BudgetCategoryID",
                table: "Expenses");
        }
    }
}
