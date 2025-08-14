using System.Linq.Expressions;

namespace Global.Sources.Abstractions.Repositories;

public interface IBooleanGenericRepository<TEntity, TId>
        where TEntity : class
        where TId : notnull
{
    Task<bool> ExistAsync(
        Expression<Func<TEntity, bool>> filter, 
        CancellationToken cancellationToken = default);
}