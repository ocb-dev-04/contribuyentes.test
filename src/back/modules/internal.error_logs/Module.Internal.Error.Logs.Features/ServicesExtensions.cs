using Microsoft.Extensions.Configuration;
using Module.Internal.Error.Logs.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace Module.Internal.Error.Logs.Features;

public static class ServicesExtensions
{
    public static IServiceCollection AddErrorLogsServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddErrorLogsPersistenceServices(configuration);

        return services;
    }
}
