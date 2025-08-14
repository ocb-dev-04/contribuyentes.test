using System.Linq.Expressions;
using Global.Sources.ResultPattern;
using Microsoft.EntityFrameworkCore;
using Module.Internal.Error.Logs.Domain.Entities;
using Module.Internal.Error.Logs.Persistence.Context;
using Module.Internal.Error.Logs.Domain.Abstractions.Repositories;

namespace Module.Internal.Error.Logs.Persistence.Repositories;

internal sealed class ErrorLogRepository
    : IErrorLogRepository
{
    private readonly ErrorLogDbContext _context;
    private readonly DbSet<ErrorLog> _table;

    public ErrorLogRepository(ErrorLogDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _table = _context.Set<ErrorLog>();
    }

    public Task<bool> ExistAsync(
        Expression<Func<ErrorLog, bool>> filter,
        CancellationToken cancellationToken = default)
        => _table.AsNoTracking()
            .TagWith($"Method name {nameof(ExistAsync)} - Repository ${nameof(ErrorLogRepository)}")
            .AnyAsync(filter, cancellationToken);

    public async Task<Result> CreateAsync(
        ErrorLog entity,
        CancellationToken cancellationToken = default)
    {
        await _context.Set<ErrorLog>().AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
