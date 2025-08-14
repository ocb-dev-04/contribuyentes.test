using Global.Sources.Models;
using System.Linq.Expressions;
using Global.Sources.Constants;
using Global.Sources.Extensions;
using Global.Sources.ResultPattern;
using Microsoft.EntityFrameworkCore;
using Global.Sources.ValueObjects.Values;
using Module.Contribuyente.Perfiles.Domain.Errors;
using Module.Contribuyente.Perfiles.Domain.Entities;
using Module.Contribuyente.Perfiles.Persistence.Context;
using Module.Contribuyente.Perfiles.Domain.Abstractions.Repositories;

namespace Module.Contribuyente.Perfiles.Persistence.Repository;

internal sealed class PerfilContribuyenteRepository
    : IPerfilContribuyenteRepository
{
    private readonly ContribuyenteDbContext _context;
    private readonly DbSet<PerfilContribuyente> _table;

    public PerfilContribuyenteRepository(ContribuyenteDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _table = _context.Set<PerfilContribuyente>();
    }

    public async Task<Result<PerfilContribuyente>> ByIdAsync(
        UlidObject id,
        bool tracking = true,
        CancellationToken cancellationToken = default)
    {
        PerfilContribuyente? found = tracking
            ? await _table.FindAsync(id, cancellationToken)
            : await _table.AsNoTracking().FirstOrDefaultAsync(f => f.Id.Equals(id), cancellationToken);
        if (found is null)
            return Result.Failure<PerfilContribuyente>(PerfilContribuyenteErrors.NotFound);

        return found;
    }

    public async Task<PaginatedCollection<PerfilContribuyente>> CollectionAsync(
        int pageNumber,
        CancellationToken cancellationToken = default)
    {
        IQueryable<PerfilContribuyente> query = _table.AsNoTracking();

        PerfilContribuyente[] collection = await query
            .OrderBy(o => o.Id)
            .Paginate(pageNumber)
            .ToArrayAsync(cancellationToken);

        int totalItems = await _table.AsNoTracking().CountAsync(cancellationToken);
        long totalPages = (totalItems + GeneralConstants.DefaultPageSize - 1) / GeneralConstants.DefaultPageSize;
        bool isLastPage = pageNumber >= totalPages;
        return PaginatedCollection<PerfilContribuyente>.Map(
            collection,
            totalItems,
            totalPages,
            isLastPage);
    }

    public Task<bool> ExistAsync(Expression<Func<PerfilContribuyente, bool>> filter, CancellationToken cancellationToken = default)
        => _table.AsNoTracking()
                .AnyAsync(filter, cancellationToken);

    public Task<bool> IsEmptyAsync(CancellationToken cancellationToken = default)
        => _table.AsNoTracking()
                .AnyAsync(cancellationToken);

    public async Task<Result> CreateAsync(PerfilContribuyente entity, CancellationToken cancellationToken = default)
    {
        await _table.AddAsync(entity, cancellationToken);
        return Result.Success();
    }

    public async Task<Result> DeleteAsync(UlidObject id, CancellationToken cancellationToken = default)
    {
        PerfilContribuyente? found = await _table.FindAsync(id, cancellationToken);
        if (found is null)
            return Result.Failure<PerfilContribuyente>(PerfilContribuyenteErrors.NotFound);

        _table.Remove(found);
        return Result.Success();
    }

    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        if (_context.ChangeTracker.HasChanges())
            await _context.SaveChangesAsync(cancellationToken);
    }
}