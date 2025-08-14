using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Module.Internal.Error.Logs.Persistence.Context;
using Module.Internal.Error.Logs.Persistence.Repositories;
using Module.Internal.Error.Logs.Domain.Abstractions.Repositories;

namespace Module.Internal.Error.Logs.Persistence;

public static class ServicesExtensions
{
    public static IServiceCollection AddErrorLogsPersistenceServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddDbContext<ErrorLogDbContext>((servicesProvider, optionsBuilder) =>
            {
                string? connectionString = configuration.GetConnectionString("GeneralDatabase");
                ArgumentException.ThrowIfNullOrEmpty(connectionString, nameof(connectionString));

                optionsBuilder.UseNpgsql(connectionString, serverOptions =>
                {
                    serverOptions.EnableRetryOnFailure(3);
                    serverOptions.CommandTimeout(5);
                    serverOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                    serverOptions.MigrationsHistoryTable("_ErrorLogDbContext_MigrationsHistory", schema: "migrations");
                });
                //optionsBuilder.UseModel(CompiledEntities.ErrorLogDbContextModel.Instance);
#if DEBUG
                optionsBuilder.EnableDetailedErrors(true);
                optionsBuilder.EnableSensitiveDataLogging(true);
#endif
            });

        services.AddScoped<IErrorLogRepository, ErrorLogRepository>();

        return services;
    }
}
