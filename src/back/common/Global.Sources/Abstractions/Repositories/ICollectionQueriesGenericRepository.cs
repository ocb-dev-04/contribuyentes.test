using Global.Sources.Models;

namespace Global.Sources.Abstractions.Repositories;

public interface ICollectionQueriesGenericRepository<TEntity>
        where TEntity : class
{
    Task<PaginatedCollection<TEntity>> CollectionAsync(
        int pageNumber, 
        CancellationToken cancellationToken = default);
}
