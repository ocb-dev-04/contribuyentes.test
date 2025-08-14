using Dapper;
using Global.Sources.Constants;
using Global.Sources.Extensions;
using Global.Sources.Models;
using Global.Sources.ResultPattern;
using Global.Sources.ValueObjects.Extensions;
using Global.Sources.ValueObjects.Values;
using Microsoft.EntityFrameworkCore;
using Modulo.Comprobante.Registros.Domain.Abstractions.Repositories;
using Modulo.Comprobante.Registros.Domain.Entities;
using Modulo.Comprobante.Registros.Persistence.Context;
using Modulo.Comprobante.Registros.Persistence.RawQueries;
using Npgsql;
using System.Data;

namespace Modulo.Comprobante.Registros.Persistence.Repositories;

internal sealed class ComprobanteRegistroRepository
      : IComprobanteRegistroRepository
{
    private readonly DapperContext _dapperContext;
    private readonly ComprobanteDbContext _context;
    private readonly DbSet<ComprobanteRegistro> _table;

    public ComprobanteRegistroRepository(
        ComprobanteDbContext context,
        DapperContext dapperContext)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _table = _context.Set<ComprobanteRegistro>();

        _dapperContext = dapperContext ?? throw new ArgumentNullException(nameof(dapperContext));
    }

    public async Task<decimal> TotalItebisSumAsync(
        UlidObject perfilContribuyenteId,
        CancellationToken cancellationToken = default)
    {
        using NpgsqlConnection connection = _dapperContext.Connection;
        await connection.OpenAsync(cancellationToken);

        decimal result = await connection.ExecuteScalarAsync<decimal>(
             new CommandDefinition(
                 ComprobantesRawQueries.ItbsSum,
                 new { PerfilId = perfilContribuyenteId.Value },
                 cancellationToken: cancellationToken
             ));

        return result;
    }

    public async Task<PaginatedCollection<ComprobanteRegistro>> CollectionAsync(
        UlidObject contribuyenteId,
        int pageNumber,
        CancellationToken cancellationToken = default)
    {
        IQueryable<ComprobanteRegistro> query = _table.AsNoTracking()
            .Where(w => w.PerfilContribuyenteId.Equals(contribuyenteId));

        ComprobanteRegistro[] collection = await query
            .OrderBy(o => o.Id)
            .Paginate(pageNumber)
            .ToArrayAsync(cancellationToken);

        int totalItems = await query.CountAsync(cancellationToken);
        long totalPages = (totalItems + GeneralConstants.DefaultPageSize - 1) / GeneralConstants.DefaultPageSize;
        bool isLastPage = pageNumber >= totalPages;
        return PaginatedCollection<ComprobanteRegistro>.Map(
            collection,
            totalItems,
            totalPages,
            isLastPage);
    }

    public Task<bool> IsEmptyAsync(CancellationToken cancellationToken = default)
        => _table.AsNoTracking()
                .AnyAsync(cancellationToken);

    public async Task<Result> CreateAsync(
        ComprobanteRegistro entity, 
        CancellationToken cancellationToken = default)
    {
        await _table.AddAsync(entity, cancellationToken);
        return Result.Success();
    }

    public async Task<Result> CreateRangeAsync(
        IEnumerable<ComprobanteRegistro> collection, 
        CancellationToken cancellationToken = default)
    {
        await _table.AddRangeAsync(collection, cancellationToken);
        return Result.Success();
    }

    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        if (_context.ChangeTracker.HasChanges())
            await _context.SaveChangesAsync(cancellationToken);
    }

}
