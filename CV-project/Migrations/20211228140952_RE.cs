using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CV_project.Migrations
{
    public partial class RE : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    AccountID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    Password = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    AccountType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.AccountID);
                });

            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    EventID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.EventID);
                });

            migrationBuilder.CreateTable(
                name: "Job",
                columns: table => new
                {
                    JobID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Job", x => x.JobID);
                });

            migrationBuilder.CreateTable(
                name: "WebCV",
                columns: table => new
                {
                    CVID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DoB = table.Column<DateTime>(type: "date", nullable: true),
                    Major = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    University = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Skills = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    BriefIntroduction = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CareerObjectives = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Awards = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Certificates = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    WorkExperience = table.Column<short>(type: "smallint", nullable: true),
                    Activities = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("WebCV_PK", x => x.CVID);
                });

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    CompanyID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NoEmployee = table.Column<short>(type: "smallint", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    AccountID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.CompanyID);
                    table.ForeignKey(
                        name: "Company_Account_FK",
                        column: x => x.AccountID,
                        principalTable: "Account",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Applicant",
                columns: table => new
                {
                    ApplicantID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DoB = table.Column<DateTime>(type: "date", nullable: true),
                    Major = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    University = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Skills = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    BriefIntroduction = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CareerObjectives = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Awards = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Certificates = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    WorkExperiences = table.Column<short>(type: "smallint", nullable: true),
                    Activities = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CVUpload = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Contact = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CVID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmailAddress = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applicant", x => x.ApplicantID);
                    table.ForeignKey(
                        name: "Applicant_Account_FK",
                        column: x => x.AccountID,
                        principalTable: "Account",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "Applicant_WebCV_FK",
                        column: x => x.CVID,
                        principalTable: "WebCV",
                        principalColumn: "CVID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HaveEvent",
                columns: table => new
                {
                    CompanyID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Venue = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EventStartDate = table.Column<DateTime>(type: "date", nullable: true),
                    EventLink = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    EventEndTime = table.Column<TimeSpan>(type: "time(5)", nullable: true),
                    EventEndDate = table.Column<DateTime>(type: "date", nullable: true),
                    EventStartTime = table.Column<TimeSpan>(type: "time(5)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("HaveEvent_PK", x => new { x.CompanyID, x.EventID });
                    table.ForeignKey(
                        name: "HaveEvent_Company_FK",
                        column: x => x.CompanyID,
                        principalTable: "Company",
                        principalColumn: "CompanyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "HaveEvent_Event_FK",
                        column: x => x.EventID,
                        principalTable: "Event",
                        principalColumn: "EventID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HaveJob",
                columns: table => new
                {
                    CompanyID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("HaveJob_PK", x => new { x.CompanyID, x.JobID });
                    table.ForeignKey(
                        name: "HaveJob_Company_FK",
                        column: x => x.CompanyID,
                        principalTable: "Company",
                        principalColumn: "CompanyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "HaveJob_Job_FK",
                        column: x => x.JobID,
                        principalTable: "Job",
                        principalColumn: "JobID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Apply",
                columns: table => new
                {
                    ApplicantID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Apply_PK", x => new { x.ApplicantID, x.CompanyID });
                    table.ForeignKey(
                        name: "Apply_Applicant_FK",
                        column: x => x.ApplicantID,
                        principalTable: "Applicant",
                        principalColumn: "ApplicantID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "Apply_Company_FK",
                        column: x => x.CompanyID,
                        principalTable: "Company",
                        principalColumn: "CompanyID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Applicant_AccountID",
                table: "Applicant",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_Applicant_CVID",
                table: "Applicant",
                column: "CVID");

            migrationBuilder.CreateIndex(
                name: "IX_Apply_CompanyID",
                table: "Apply",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_Company_AccountID",
                table: "Company",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_HaveEvent_EventID",
                table: "HaveEvent",
                column: "EventID");

            migrationBuilder.CreateIndex(
                name: "IX_HaveJob_JobID",
                table: "HaveJob",
                column: "JobID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Apply");

            migrationBuilder.DropTable(
                name: "HaveEvent");

            migrationBuilder.DropTable(
                name: "HaveJob");

            migrationBuilder.DropTable(
                name: "Applicant");

            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropTable(
                name: "Job");

            migrationBuilder.DropTable(
                name: "WebCV");

            migrationBuilder.DropTable(
                name: "Account");
        }
    }
}
