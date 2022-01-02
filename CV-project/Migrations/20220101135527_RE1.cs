using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CV_project.Migrations
{
    public partial class RE1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Vacancy",
                table: "Job",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Deadline",
                table: "HaveJob",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Vacancy",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "Deadline",
                table: "HaveJob");
        }
    }
}
