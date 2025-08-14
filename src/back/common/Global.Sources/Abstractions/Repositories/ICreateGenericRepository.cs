using Global.Sources.ResultPattern;

namespace Global.Sources.Abstractions.Repositories;

public interface ICreateGenericRepository<TEntity>
    where TEntity : class
{
    Task<Result> CreateAsync(
        TEntity entity, 
        CancellationToken cancellationToken = default);
}
