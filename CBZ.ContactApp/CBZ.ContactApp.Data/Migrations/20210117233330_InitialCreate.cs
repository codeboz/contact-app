using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CBZ.ContactApp.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Surname = table.Column<string>(type: "text", nullable: false),
                    Company = table.Column<string>(type: "text", nullable: true),
                    Inserted = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()"),
                    Updated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InfoTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Inserted = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()"),
                    Updated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InfoTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReportStates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Inserted = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()"),
                    Updated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportStates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Infos",
                columns: table => new
                {
                    InfoTypeId = table.Column<int>(type: "integer", nullable: false),
                    ContactId = table.Column<Guid>(type: "uuid", nullable: false),
                    Data = table.Column<string>(type: "text", nullable: false),
                    Inserted = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()"),
                    Updated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Infos", x => new { x.ContactId, x.InfoTypeId });
                    table.ForeignKey(
                        name: "FK_Infos_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Infos_InfoTypes_InfoTypeId",
                        column: x => x.InfoTypeId,
                        principalTable: "InfoTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReportRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Location = table.Column<string>(type: "text", nullable: true),
                    Requested = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()"),
                    ReportStateId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportRequests_ReportStates_ReportStateId",
                        column: x => x.ReportStateId,
                        principalTable: "ReportStates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "Company", "Name", "Surname" },
                values: new object[,]
                {
                    { new Guid("3999bbc4-751e-4fe5-9e72-4c6e88f6cff4"), "A", "A", "Aa" },
                    { new Guid("3182b605-a8cb-4eb4-81fb-81129159f095"), "B", "B", "Bb" },
                    { new Guid("b75cdf77-243b-4eb8-ab08-c44dcddb70ef"), "C", "C", "Cc" },
                    { new Guid("342ea3e1-6e66-4162-a7c0-390c2a5aa263"), "D", "D", "Dd" },
                    { new Guid("19c45392-88ba-4f96-92d4-c72024d48f81"), "E", "E", "Ee" }
                });

            migrationBuilder.InsertData(
                table: "InfoTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Phone" },
                    { 2, "Email" },
                    { 3, "Location" }
                });

            migrationBuilder.InsertData(
                table: "ReportStates",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Preparing" },
                    { 2, "Ready" }
                });

            migrationBuilder.InsertData(
                table: "Infos",
                columns: new[] { "ContactId", "InfoTypeId", "Data" },
                values: new object[,]
                {
                    { new Guid("3999bbc4-751e-4fe5-9e72-4c6e88f6cff4"), 1, "11111111" },
                    { new Guid("19c45392-88ba-4f96-92d4-c72024d48f81"), 3, "Bursa" },
                    { new Guid("342ea3e1-6e66-4162-a7c0-390c2a5aa263"), 3, "Bursa" },
                    { new Guid("b75cdf77-243b-4eb8-ab08-c44dcddb70ef"), 3, "Ankara" },
                    { new Guid("3182b605-a8cb-4eb4-81fb-81129159f095"), 3, "Ankara" },
                    { new Guid("3999bbc4-751e-4fe5-9e72-4c6e88f6cff4"), 3, "Ankara" },
                    { new Guid("19c45392-88ba-4f96-92d4-c72024d48f81"), 2, "e@e.com" },
                    { new Guid("342ea3e1-6e66-4162-a7c0-390c2a5aa263"), 2, "d@d.com" },
                    { new Guid("3182b605-a8cb-4eb4-81fb-81129159f095"), 2, "b@b.com" },
                    { new Guid("3999bbc4-751e-4fe5-9e72-4c6e88f6cff4"), 2, "a@a.com" },
                    { new Guid("19c45392-88ba-4f96-92d4-c72024d48f81"), 1, "555555555" },
                    { new Guid("342ea3e1-6e66-4162-a7c0-390c2a5aa263"), 1, "4444444444" },
                    { new Guid("b75cdf77-243b-4eb8-ab08-c44dcddb70ef"), 1, "333333333" },
                    { new Guid("3182b605-a8cb-4eb4-81fb-81129159f095"), 1, "222222222" },
                    { new Guid("b75cdf77-243b-4eb8-ab08-c44dcddb70ef"), 2, "c@c.com" }
                });

            migrationBuilder.InsertData(
                table: "ReportRequests",
                columns: new[] { "Id", "Location", "ReportStateId" },
                values: new object[,]
                {
                    { new Guid("9fcb7cd9-6e2a-40eb-b1bb-5a052de8ad8d"), "Ankara", 1 },
                    { new Guid("045f216b-0e1c-460d-8108-1084d61abe3f"), "Bursa", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_Name_Surname",
                table: "Contacts",
                columns: new[] { "Name", "Surname" });

            migrationBuilder.CreateIndex(
                name: "IX_Infos_InfoTypeId",
                table: "Infos",
                column: "InfoTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_InfoTypes_Name",
                table: "InfoTypes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReportRequests_ReportStateId",
                table: "ReportRequests",
                column: "ReportStateId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportStates_Name",
                table: "ReportStates",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Infos");

            migrationBuilder.DropTable(
                name: "ReportRequests");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "InfoTypes");

            migrationBuilder.DropTable(
                name: "ReportStates");
        }
    }
}
