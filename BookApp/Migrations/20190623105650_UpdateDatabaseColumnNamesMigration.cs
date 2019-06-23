using Microsoft.EntityFrameworkCore.Migrations;

namespace BookApp.Migrations
{
    public partial class UpdateDatabaseColumnNamesMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FisrtName",
                table: "Reviewers",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "FisrtName",
                table: "Authors",
                newName: "FirstName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Reviewers",
                newName: "FisrtName");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Authors",
                newName: "FisrtName");
        }
    }
}
