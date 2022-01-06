using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CV_project.Migrations
{
    public partial class RE12243 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HeadHunt_Account_AccountID",
                table: "HeadHunt");

            migrationBuilder.DropIndex(
                name: "IX_HeadHunt_AccountID",
                table: "HeadHunt");

            migrationBuilder.DropColumn(
                name: "AccountID",
                table: "HeadHunt");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AccountID",
                table: "HeadHunt",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_HeadHunt_AccountID",
                table: "HeadHunt",
                column: "AccountID");

            migrationBuilder.AddForeignKey(
                name: "FK_HeadHunt_Account_AccountID",
                table: "HeadHunt",
                column: "AccountID",
                principalTable: "Account",
                principalColumn: "AccountID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
