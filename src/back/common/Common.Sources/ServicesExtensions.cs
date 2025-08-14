using Common.Sources.Abstractions;
using Common.Sources.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Sources;

public static class ServicesExtensions
{
    public static IServiceCollection AddUnitOfWorkServices(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfwork, UnitOfWork>();

        return services;
    }
}
