using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Module.Contribuyente.Perfiles.Persistence.Context;
using Module.Contribuyente.Perfiles.Persistence.Repository;
using Module.Contribuyente.Perfiles.Domain.Abstractions.Repositories;

namespace Module.Contribuyente.Perfiles.Persistence;

public static class ServicesExtensions
{
    public static IServiceCollection AddContribuyentePersistenceServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddDbContext<ContribuyenteDbContext>((servicesProvider, optionsBuilder) =>
            {
                string? connectionString = configuration.GetConnectionString("GeneralDatabase");
                ArgumentException.ThrowIfNullOrEmpty(connectionString, nameof(connectionString));

                optionsBuilder.UseNpgsql(connectionString, serverOptions =>
                {
                    serverOptions.EnableRetryOnFailure(3);
                    serverOptions.CommandTimeout(5);
                    serverOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                    serverOptions.MigrationsHistoryTable("_ContribuyenteDbContext_MigrationsHistory", schema: "migrations");
                });
                //optionsBuilder.UseModel(CompiledEntities.ContribuyenteDbContextModel.Instance);
#if DEBUG
                optionsBuilder.EnableDetailedErrors(true);
                optionsBuilder.EnableSensitiveDataLogging(true);
#endif
            });

        services.AddScoped<IPerfilContribuyenteRepository, PerfilContribuyenteRepository>();

        return services;
    }
}
