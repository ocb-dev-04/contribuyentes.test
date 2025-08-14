using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modulo.Comprobante.Registros.Domain.Abstractions.Repositories;
using Modulo.Comprobante.Registros.Persistence.Context;
using Modulo.Comprobante.Registros.Persistence.Repositories;
using Npgsql;
using System.Data;

namespace Modulo.Comprobante.Registros.Persistence;

public static class ServicesExtensions
{
    public static IServiceCollection AddComprobantePersistenceServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddDbContext<ComprobanteDbContext>((servicesProvider, optionsBuilder) =>
            {
                string? connectionString = configuration.GetConnectionString("GeneralDatabase");
                ArgumentException.ThrowIfNullOrEmpty(connectionString, nameof(connectionString));

                optionsBuilder.UseNpgsql(connectionString, serverOptions =>
                {
                    serverOptions.EnableRetryOnFailure(3);
                    serverOptions.CommandTimeout(5);
                    serverOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                    serverOptions.MigrationsHistoryTable("_ComprobanteDbContext_MigrationsHistory", schema: "migrations");
                });
                //optionsBuilder.UseModel(CompiledEntities.ComprobanteDbContextModel.Instance);
#if DEBUG
                optionsBuilder.EnableDetailedErrors(true);
                optionsBuilder.EnableSensitiveDataLogging(true);
#endif
            });

        services.AddScoped<DapperContext>(sp =>
        {
            IConfiguration configuration = sp.GetRequiredService<IConfiguration>();
            string? connectionString = configuration.GetConnectionString("GeneralDatabase");
            ArgumentException.ThrowIfNullOrEmpty(connectionString, nameof(connectionString));

            return new DapperContext(connectionString);
        });
        services.AddScoped<IComprobanteRegistroRepository, ComprobanteRegistroRepository>();

        return services;
    }
}
