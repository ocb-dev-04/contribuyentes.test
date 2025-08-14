using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modulo.Comprobante.Registros.Persistence;

namespace Modulo.Comprobante.Registros.Features;

public static class ServicesExtensions
{
    public static IServiceCollection AddComprobanteServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddComprobantePersistenceServices(configuration);

        return services;
    }
}
