using Global.Sources.ValueObjects.Values;
using Global.Sources.Abstractions.Repositories;
using Module.Contribuyente.Perfiles.Domain.Entities;

namespace Module.Contribuyente.Perfiles.Domain.Abstractions.Repositories;

public interface IPerfilContribuyenteRepository
    : ISingleQueriesGenericRepository<PerfilContribuyente, UlidObject>,
        IBooleanGenericRepository<PerfilContribuyente, UlidObject>,
        ICollectionQueriesGenericRepository<PerfilContribuyente>,
        ICreateGenericRepository<PerfilContribuyente>,
        IDeleteGenericRepository<PerfilContribuyente, UlidObject>
{
    Task<bool> IsEmptyAsync(CancellationToken cancellationToken = default);
    Task CommitAsync(CancellationToken cancellationToken = default);
}