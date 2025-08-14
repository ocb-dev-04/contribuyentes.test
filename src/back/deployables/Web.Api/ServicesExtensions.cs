using Common.Sources;
using Global.Sources.Constants;
using Presentation;

namespace Web.Api;

public static class ServicesExtensions
{
    public static IServiceCollection AddServiceCollection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOpenApi();
        services.AddControllers()
            .AddApplicationPart(typeof(Presentation.ServicesExtensions).Assembly);

#if DEBUG
        services.AddCors(options =>
        {
            options.AddPolicy(GeneralConstants.FrontEndPortCorsName,
                policy =>
                {
                    policy.WithOrigins("http://localhost:3000")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
        });
#endif

        services.AddPresentationServices(configuration)
            .AddUnitOfWorkServices();

        return services;
    }
}
