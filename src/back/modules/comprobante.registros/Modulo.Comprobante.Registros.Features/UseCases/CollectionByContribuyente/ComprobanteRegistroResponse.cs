using Modulo.Comprobante.Registros.Domain.Entities;

namespace Modulo.Comprobante.Registros.Features.UseCases.CollectionByContribuyente;

public sealed record ComprobanteRegistroResponse(
        Ulid perfilContribuyenteId,
        string ncf,
        decimal monto,
        decimal Itebis18)
{
    public static ComprobanteRegistroResponse MapFromEntity(ComprobanteRegistro entity)
        => new(
            entity.Id.AsUlid,
            entity.NCF.Value,
            entity.Monto.Value,
            entity.Itbis18.Value);
}