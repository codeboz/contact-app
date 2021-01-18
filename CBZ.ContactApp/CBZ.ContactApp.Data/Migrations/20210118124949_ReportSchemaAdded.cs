using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CBZ.ContactApp.Data.Migrations
{
    public partial class ReportSchemaAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ReportRequests",
                keyColumn: "Id",
                keyValue: new Guid("045f216b-0e1c-460d-8108-1084d61abe3f"));

            migrationBuilder.DeleteData(
                table: "ReportRequests",
                keyColumn: "Id",
                keyValue: new Guid("9fcb7cd9-6e2a-40eb-b1bb-5a052de8ad8d"));

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ReportStates",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:IdentitySequenceOptions", "'100', '1', '', '', 'False', '1'")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "ReportId",
                table: "ReportRequests",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "InfoTypes",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:IdentitySequenceOptions", "'100', '1', '', '', 'False', '1'")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:IdentitySequenceOptions", "'100', '1', '', '', 'False', '1'")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ContactCount = table.Column<int>(type: "integer", nullable: false),
                    PhoneNumberCount = table.Column<int>(type: "integer", nullable: false),
                    Location = table.Column<string>(type: "text", nullable: false),
                    Inserted = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()"),
                    Updated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Reports",
                columns: new[] { "Id", "ContactCount", "Location", "PhoneNumberCount" },
                values: new object[,]
                {
                    { 1, 3, "Ankara", 3 },
                    { 2, 2, "Bursa", 2 }
                });

            migrationBuilder.InsertData(
                table: "ReportRequests",
                columns: new[] { "Id", "Location", "ReportId", "ReportStateId" },
                values: new object[,]
                {
                    { new Guid("bf5bd4b9-ebf5-41a8-a597-69b7aeb41432"), "Ankara", 1, 1 },
                    { new Guid("4c802ae2-5292-4063-b5ca-e09b18871143"), "Bursa", 2, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReportRequests_ReportId",
                table: "ReportRequests",
                column: "ReportId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReportRequests_Reports_ReportId",
                table: "ReportRequests",
                column: "ReportId",
                principalTable: "Reports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReportRequests_Reports_ReportId",
                table: "ReportRequests");

            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_ReportRequests_ReportId",
                table: "ReportRequests");

            migrationBuilder.DeleteData(
                table: "ReportRequests",
                keyColumn: "Id",
                keyValue: new Guid("4c802ae2-5292-4063-b5ca-e09b18871143"));

            migrationBuilder.DeleteData(
                table: "ReportRequests",
                keyColumn: "Id",
                keyValue: new Guid("bf5bd4b9-ebf5-41a8-a597-69b7aeb41432"));

            migrationBuilder.DropColumn(
                name: "ReportId",
                table: "ReportRequests");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ReportStates",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:IdentitySequenceOptions", "'100', '1', '', '', 'False', '1'")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "InfoTypes",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:IdentitySequenceOptions", "'100', '1', '', '', 'False', '1'")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.InsertData(
                table: "ReportRequests",
                columns: new[] { "Id", "Location", "ReportStateId" },
                values: new object[,]
                {
                    { new Guid("9fcb7cd9-6e2a-40eb-b1bb-5a052de8ad8d"), "Ankara", 1 },
                    { new Guid("045f216b-0e1c-460d-8108-1084d61abe3f"), "Bursa", 2 }
                });
        }
    }
}
