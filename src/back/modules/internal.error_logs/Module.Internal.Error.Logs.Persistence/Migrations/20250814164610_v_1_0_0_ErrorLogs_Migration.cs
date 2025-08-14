using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Module.Internal.Error.Logs.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class v_1_0_0_ErrorLogs_Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "internal");

            migrationBuilder.CreateTable(
                name: "ErrorLog",
                schema: "internal",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(26)", maxLength: 26, nullable: false),
                    IpAddress = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Path = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Controller = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Action = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Method = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    InnerException = table.Column<string>(type: "text", nullable: false),
                    StackTrace = table.Column<string>(type: "text", nullable: false),
                    CreatedOnUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErrorLog", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ErrorLog_Method_Action_Controller_Path_IpAddress",
                schema: "internal",
                table: "ErrorLog",
                columns: new[] { "Method", "Action", "Controller", "Path", "IpAddress" })
                .Annotation("Npgsql:IndexMethod", "Btree");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ErrorLog",
                schema: "internal");
        }
    }
}
