using Global.Sources.ValueObjects.Values;

namespace Modulo.Comprobante.Registros.Domain.Entities;

public sealed partial class ComprobanteRegistro
{
    public static ComprobanteRegistro Create(
        UlidObject id,
        UlidObject perfilContribuyenteId,
        StringObject ncf,
        DecimalObject monto)
        => new(id, perfilContribuyenteId, ncf, monto);
}