using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Module.Contribuyente.Perfiles.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class v_1_0_0_Contribuyentes_Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "contribuyentes");

            migrationBuilder.CreateTable(
                name: "PerfilContribuyente",
                schema: "contribuyentes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(26)", maxLength: 26, nullable: false),
                    Tipo = table.Column<int>(type: "integer", nullable: false),
                    CreadoEnUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    RncCedula = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    EstaActivo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerfilContribuyente", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PerfilContribuyente_Nombre_Tipo",
                schema: "contribuyentes",
                table: "PerfilContribuyente",
                columns: new[] { "Nombre", "Tipo" })
                .Annotation("Npgsql:IndexMethod", "Btree");

            migrationBuilder.CreateIndex(
                name: "IX_PerfilContribuyente_RncCedula",
                schema: "contribuyentes",
                table: "PerfilContribuyente",
                column: "RncCedula",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PerfilContribuyente",
                schema: "contribuyentes");
        }
    }
}
