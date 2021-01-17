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
                columns: new[] { "Id", "Company", "Inserted", "Name", "Surname" },
                values: new object[,]
                {
                    { new Guid("3999bbc4-751e-4fe5-9e72-4c6e88f6cff4"), "A", new DateTime(2021, 1, 18, 1, 2, 49, 851, DateTimeKind.Local).AddTicks(4158), "A", "Aa" },
                    { new Guid("3182b605-a8cb-4eb4-81fb-81129159f095"), "B", new DateTime(2021, 1, 18, 1, 2, 49, 852, DateTimeKind.Local).AddTicks(1154), "B", "Bb" },
                    { new Guid("b75cdf77-243b-4eb8-ab08-c44dcddb70ef"), "C", new DateTime(2021, 1, 18, 1, 2, 49, 852, DateTimeKind.Local).AddTicks(1160), "C", "Cc" },
                    { new Guid("342ea3e1-6e66-4162-a7c0-390c2a5aa263"), "D", new DateTime(2021, 1, 18, 1, 2, 49, 852, DateTimeKind.Local).AddTicks(1164), "D", "Dd" },
                    { new Guid("19c45392-88ba-4f96-92d4-c72024d48f81"), "E", new DateTime(2021, 1, 18, 1, 2, 49, 852, DateTimeKind.Local).AddTicks(1167), "E", "Ee" }
                });

            migrationBuilder.InsertData(
                table: "InfoTypes",
                columns: new[] { "Id", "Inserted", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 1, 18, 1, 2, 49, 855, DateTimeKind.Local).AddTicks(6330), "Phone" },
                    { 2, new DateTime(2021, 1, 18, 1, 2, 49, 855, DateTimeKind.Local).AddTicks(6811), "Email" },
                    { 3, new DateTime(2021, 1, 18, 1, 2, 49, 855, DateTimeKind.Local).AddTicks(6815), "Location" }
                });

            migrationBuilder.InsertData(
                table: "ReportStates",
                columns: new[] { "Id", "Inserted", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 1, 18, 1, 2, 49, 861, DateTimeKind.Local).AddTicks(2744), "Preparing" },
                    { 2, new DateTime(2021, 1, 18, 1, 2, 49, 861, DateTimeKind.Local).AddTicks(2786), "Ready" }
                });

            migrationBuilder.InsertData(
                table: "Infos",
                columns: new[] { "ContactId", "InfoTypeId", "Data", "Inserted" },
                values: new object[] { new Guid("3999bbc4-751e-4fe5-9e72-4c6e88f6cff4"), 1, "11111111", new DateTime(2021, 1, 18, 1, 2, 49, 859, DateTimeKind.Local).AddTicks(7674) });

            migrationBuilder.InsertData(
                table: "Infos",
                columns: new[] { "ContactId", "InfoTypeId", "Data" },
                values: new object[,]
                {
                    { new Guid("19c45392-88ba-4f96-92d4-c72024d48f81"), 3, "Bursa" },
                    { new Guid("342ea3e1-6e66-4162-a7c0-390c2a5aa263"), 3, "Bursa" }
                });

            migrationBuilder.InsertData(
                table: "Infos",
                columns: new[] { "ContactId", "InfoTypeId", "Data", "Inserted" },
                values: new object[,]
                {
                    { new Guid("b75cdf77-243b-4eb8-ab08-c44dcddb70ef"), 3, "Ankara", new DateTime(2021, 1, 18, 1, 2, 49, 859, DateTimeKind.Local).AddTicks(8265) },
                    { new Guid("3182b605-a8cb-4eb4-81fb-81129159f095"), 3, "Ankara", new DateTime(2021, 1, 18, 1, 2, 49, 859, DateTimeKind.Local).AddTicks(8255) },
                    { new Guid("3999bbc4-751e-4fe5-9e72-4c6e88f6cff4"), 3, "Ankara", new DateTime(2021, 1, 18, 1, 2, 49, 859, DateTimeKind.Local).AddTicks(8240) }
                });

            migrationBuilder.InsertData(
                table: "Infos",
                columns: new[] { "ContactId", "InfoTypeId", "Data" },
                values: new object[,]
                {
                    { new Guid("19c45392-88ba-4f96-92d4-c72024d48f81"), 2, "e@e.com" },
                    { new Guid("342ea3e1-6e66-4162-a7c0-390c2a5aa263"), 2, "d@d.com" }
                });

            migrationBuilder.InsertData(
                table: "Infos",
                columns: new[] { "ContactId", "InfoTypeId", "Data", "Inserted" },
                values: new object[,]
                {
                    { new Guid("3182b605-a8cb-4eb4-81fb-81129159f095"), 2, "b@b.com", new DateTime(2021, 1, 18, 1, 2, 49, 859, DateTimeKind.Local).AddTicks(8247) },
                    { new Guid("3999bbc4-751e-4fe5-9e72-4c6e88f6cff4"), 2, "a@a.com", new DateTime(2021, 1, 18, 1, 2, 49, 859, DateTimeKind.Local).AddTicks(8235) }
                });

            migrationBuilder.InsertData(
                table: "Infos",
                columns: new[] { "ContactId", "InfoTypeId", "Data" },
                values: new object[,]
                {
                    { new Guid("19c45392-88ba-4f96-92d4-c72024d48f81"), 1, "555555555" },
                    { new Guid("342ea3e1-6e66-4162-a7c0-390c2a5aa263"), 1, "4444444444" }
                });

            migrationBuilder.InsertData(
                table: "Infos",
                columns: new[] { "ContactId", "InfoTypeId", "Data", "Inserted" },
                values: new object[,]
                {
                    { new Guid("b75cdf77-243b-4eb8-ab08-c44dcddb70ef"), 1, "333333333", new DateTime(2021, 1, 18, 1, 2, 49, 859, DateTimeKind.Local).AddTicks(8259) },
                    { new Guid("3182b605-a8cb-4eb4-81fb-81129159f095"), 1, "222222222", new DateTime(2021, 1, 18, 1, 2, 49, 859, DateTimeKind.Local).AddTicks(8243) },
                    { new Guid("b75cdf77-243b-4eb8-ab08-c44dcddb70ef"), 2, "c@c.com", new DateTime(2021, 1, 18, 1, 2, 49, 859, DateTimeKind.Local).AddTicks(8262) }
                });

            migrationBuilder.InsertData(
                table: "ReportRequests",
                columns: new[] { "Id", "Location", "ReportStateId" },
                values: new object[,]
                {
                    { new Guid("81ab94a2-891f-4a85-a6b8-b19a68e2cd26"), "Ankara", 1 },
                    { new Guid("c97fa77b-575e-4619-a7b1-b8282ad225f1"), "Bursa", 2 }
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
