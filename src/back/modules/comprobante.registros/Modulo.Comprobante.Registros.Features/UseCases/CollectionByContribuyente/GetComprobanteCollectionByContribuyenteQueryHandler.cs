using MediatR;
using Global.Sources.Models;
using Common.Sources.Abstractions;
using Global.Sources.ResultPattern;
using Global.Sources.ValueObjects.Values;
using Modulo.Comprobante.Registros.Domain.Entities;

namespace Modulo.Comprobante.Registros.Features.UseCases.CollectionByContribuyente;

internal sealed class GetComprobanteCollectionByContribuyenteQueryHandler
    : IRequestHandler<GetComprobanteCollectionByContribuyenteQuery, Result<PaginatedCollection<ComprobanteRegistroResponse>>>
{
    private readonly IUnitOfwork _unitOfwork;

    public GetComprobanteCollectionByContribuyenteQueryHandler(IUnitOfwork unitOfwork)
        => _unitOfwork = unitOfwork ?? throw new ArgumentNullException(nameof(unitOfwork));

    public async Task<Result<PaginatedCollection<ComprobanteRegistroResponse>>> Handle(GetComprobanteCollectionByContribuyenteQuery request, CancellationToken cancellationToken)
    {
        UlidObject contribuyenteIdAsObject = UlidObject.Create(request.ContribuyenteId);
        PaginatedCollection<ComprobanteRegistro> collection = await _unitOfwork.ComprobanteRegistro.CollectionAsync(
            contribuyenteIdAsObject,
            request.PageNumber,
            cancellationToken);

        return collection.MapData(
            x => ComprobanteRegistroResponse.MapFromEntity(x));
    }
}