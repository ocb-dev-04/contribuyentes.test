namespace Modulo.Comprobante.Registros.Persistence.RawQueries;

internal static class ComprobantesRawQueries
{
    public const string ItbsSum = @"
        SELECT COALESCE(SUM(""Itbis18""), 0)
        FROM ""comprobantes"".""ComprobanteRegistro""
        WHERE ""PerfilContribuyenteId"" = @PerfilId";
}
