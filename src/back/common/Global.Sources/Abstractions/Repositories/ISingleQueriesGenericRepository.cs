using Global.Sources.ResultPattern;

namespace Global.Sources.Abstractions.Repositories;

public interface ISingleQueriesGenericRepository<TEntity, TId>
        where TEntity : class
        where TId : notnull
{
    Task<Result<TEntity>> ByIdAsync(
        TId id, 
        bool tracking = true,
        CancellationToken cancellationToken = default);
}