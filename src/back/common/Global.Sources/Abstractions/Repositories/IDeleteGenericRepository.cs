using Global.Sources.ResultPattern;

namespace Global.Sources.Abstractions.Repositories;

public interface IDeleteGenericRepository<TEntity, TId>
        where TEntity : class
        where TId : notnull
{
    Task<Result> DeleteAsync(
        TId id, 
        CancellationToken cancellationToken = default);
}