using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Module.Internal.Error.Logs.Persistence.Context;

namespace Module.Internal.Error.Logs.Persistence;

public static class BuilderExtensions
{
    public static WebApplication CheckErrorLogMigrations(this WebApplication app)
    {
        using IServiceScope scope = app.Services.CreateScope();
        using ErrorLogDbContext context = scope.ServiceProvider.GetRequiredService<ErrorLogDbContext>();

        bool canConnect = context.Database.CanConnect();
        if (!canConnect)
            throw new Exception("Can't connect to database");

        IEnumerable<string> pendingMigrations = context.Database.GetPendingMigrations();
        if (pendingMigrations.Any())
            context.Database.Migrate();

        return app;
    }
}
