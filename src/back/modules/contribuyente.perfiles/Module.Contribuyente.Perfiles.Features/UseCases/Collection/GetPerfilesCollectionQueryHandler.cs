using Common.Sources.Abstractions;
using Global.Sources.Models;
using Global.Sources.ResultPattern;
using Global.Sources.ValueObjects.Values;
using MediatR;
using Module.Contribuyente.Perfiles.Domain.Entities;

namespace Module.Contribuyente.Perfiles.Features.UseCases.Collection;

internal sealed class GetPerfilesCollectionQueryHandler
    : IRequestHandler<GetPerfilesCollectionQuery, Result<PaginatedCollection<GetPerfilesCollectionQueryResponse>>>
{
    private readonly IUnitOfwork _unitOfwork;

    public GetPerfilesCollectionQueryHandler(IUnitOfwork unitOfwork)
        => _unitOfwork = unitOfwork ?? throw new ArgumentNullException(nameof(unitOfwork));

    public async Task<Result<PaginatedCollection<GetPerfilesCollectionQueryResponse>>> Handle(GetPerfilesCollectionQuery request, CancellationToken cancellationToken)
    {
        PaginatedCollection<PerfilContribuyente> collection = await _unitOfwork.PerfilContribuyente.CollectionAsync(
            request.PageNumber,
            cancellationToken);

        return collection.MapData(
            x => GetPerfilesCollectionQueryResponse.MapFromEntity(x));
    }
}
