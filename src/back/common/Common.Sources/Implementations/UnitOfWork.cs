using Common.Sources.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Modulo.Comprobante.Registros.Domain.Abstractions.Repositories;
using Module.Contribuyente.Perfiles.Domain.Abstractions.Repositories;

namespace Common.Sources.Implementations;

internal sealed class UnitOfWork
    : IUnitOfwork
{
    private readonly IServiceScope _serviceScope;

    public UnitOfWork(IServiceScopeFactory scopeFactory)
    {
        IServiceScopeFactory scope = scopeFactory ?? throw new ArgumentNullException(nameof(scopeFactory));
        _serviceScope = scopeFactory.CreateScope();
    }

    private IPerfilContribuyenteRepository? _perfilContribuyenteRepository;
    public IPerfilContribuyenteRepository PerfilContribuyente
    {
        get => _perfilContribuyenteRepository ??= _serviceScope.ServiceProvider.GetRequiredService<IPerfilContribuyenteRepository>();
    }

    private IComprobanteRegistroRepository? _comprobanteRegistroRepository;
    public IComprobanteRegistroRepository ComprobanteRegistro
    {
        get => _comprobanteRegistroRepository ??= _serviceScope.ServiceProvider.GetRequiredService<IComprobanteRegistroRepository>();
    }

    public void Dispose()
        => _serviceScope.Dispose();
}
