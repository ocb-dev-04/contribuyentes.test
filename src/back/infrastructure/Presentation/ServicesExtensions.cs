using FluentValidation;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Presentation.Behaviors.Pipelines;
using Microsoft.Extensions.Configuration;
using Module.Internal.Error.Logs.Features;
using Modulo.Comprobante.Registros.Features;
using Module.Contribuyente.Perfiles.Features;
using Microsoft.Extensions.DependencyInjection;

namespace Presentation;

public static  class ServicesExtensions
{
    public static IServiceCollection AddPresentationServices(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

        services
            .AddContribuyenteServices(configuration)
            .AddComprobanteServices(configuration)
            .AddErrorLogsServices(configuration);

        Assembly[] applicationAssemblies = typeof(ServicesExtensions).Assembly
                .GetReferencedAssemblies()
                .Where(w => w.Name!.EndsWith(".Features"))
                .Select(s => Assembly.Load(s.Name!))
                .ToArray();

        services.AddValidatorsFromAssemblies(
            applicationAssemblies,
            includeInternalTypes: true);

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(applicationAssemblies);

            config.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
        });

        return services;
    }
}
