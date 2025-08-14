using Global.Sources.Models;
using Global.Sources.ResultPattern;
using Global.Sources.ValueObjects.Values;
using Global.Sources.Abstractions.Repositories;
using Modulo.Comprobante.Registros.Domain.Entities;

namespace Modulo.Comprobante.Registros.Domain.Abstractions.Repositories;

public interface IComprobanteRegistroRepository
    : ICreateGenericRepository<ComprobanteRegistro>
{
    Task<decimal> TotalItebisSumAsync(
        UlidObject perfilContribuyenteId, 
        CancellationToken cancellationToken = default);
    Task<PaginatedCollection<ComprobanteRegistro>> CollectionAsync(
        UlidObject contribuyenteId,
        int pageNumber,
        CancellationToken cancellationToken = default);
    Task<bool> IsEmptyAsync(CancellationToken cancellationToken = default);
    Task<Result> CreateRangeAsync(IEnumerable<ComprobanteRegistro> collection, CancellationToken cancellationToken = default);
    Task CommitAsync(CancellationToken cancellationToken = default);
}