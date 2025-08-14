using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modulo.Comprobante.Registros.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class v_1_0_0_Comprobantes_Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "comprobantes");

            migrationBuilder.CreateTable(
                name: "ComprobanteRegistro",
                schema: "comprobantes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(26)", maxLength: 26, nullable: false),
                    PerfilContribuyenteId = table.Column<string>(type: "character varying(26)", maxLength: 26, nullable: false),
                    NCF = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    Monto = table.Column<decimal>(type: "numeric(16,2)", precision: 16, scale: 2, nullable: false),
                    Itbis18 = table.Column<decimal>(type: "numeric(16,2)", precision: 16, scale: 2, nullable: false),
                    CreadoEnUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComprobanteRegistro", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComprobanteRegistro_PerfilContribuyenteId_NCF_Monto",
                schema: "comprobantes",
                table: "ComprobanteRegistro",
                columns: new[] { "PerfilContribuyenteId", "NCF", "Monto" })
                .Annotation("Npgsql:IndexMethod", "Btree");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComprobanteRegistro",
                schema: "comprobantes");
        }
    }
}
