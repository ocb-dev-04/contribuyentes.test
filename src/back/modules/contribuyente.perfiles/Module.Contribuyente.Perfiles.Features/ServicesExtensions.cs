using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Module.Contribuyente.Perfiles.Persistence;

namespace Module.Contribuyente.Perfiles.Features;

public static class ServicesExtensions
{
    public static IServiceCollection AddContribuyenteServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddContribuyentePersistenceServices(configuration);

        return services;
    }
}
