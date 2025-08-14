using Module.Contribuyente.Perfiles.Domain.Abstractions.Repositories;
using Modulo.Comprobante.Registros.Domain.Abstractions.Repositories;

namespace Common.Sources.Abstractions;

public interface IUnitOfwork : IDisposable
{
    IPerfilContribuyenteRepository PerfilContribuyente { get; }
    IComprobanteRegistroRepository ComprobanteRegistro { get; }
}
