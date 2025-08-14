using Global.Sources.Models;
using Global.Sources.Abstractions.Messaging;

namespace Modulo.Comprobante.Registros.Features.UseCases.CollectionByContribuyente;

public sealed record GetComprobanteCollectionByContribuyenteQuery(
    string ContribuyenteId,
    int PageNumber) : IQuery<PaginatedCollection<ComprobanteRegistroResponse>>;
