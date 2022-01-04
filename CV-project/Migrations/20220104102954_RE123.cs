using Microsoft.EntityFrameworkCore.Migrations;

namespace CV_project.Migrations
{
    public partial class RE123 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "HaveJob",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "HaveJob",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumEmployee",
                table: "HaveJob",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Qualification",
                table: "HaveJob",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Requirement",
                table: "HaveJob",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SalaryPerMonth",
                table: "HaveJob",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Worktime",
                table: "HaveJob",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "HaveJob");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "HaveJob");

            migrationBuilder.DropColumn(
                name: "NumEmployee",
                table: "HaveJob");

            migrationBuilder.DropColumn(
                name: "Qualification",
                table: "HaveJob");

            migrationBuilder.DropColumn(
                name: "Requirement",
                table: "HaveJob");

            migrationBuilder.DropColumn(
                name: "SalaryPerMonth",
                table: "HaveJob");

            migrationBuilder.DropColumn(
                name: "Worktime",
                table: "HaveJob");
        }
    }
}
