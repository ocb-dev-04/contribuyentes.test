using Global.Sources.Models;
using Global.Sources.Abstractions.Messaging;

namespace Module.Contribuyente.Perfiles.Features.UseCases.Collection;

public sealed record GetPerfilesCollectionQuery(
    int PageNumber) : IQuery<PaginatedCollection<GetPerfilesCollectionQueryResponse>>;
