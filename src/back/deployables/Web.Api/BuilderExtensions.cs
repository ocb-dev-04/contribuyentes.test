using Module.Internal.Error.Logs.Persistence;
using Modulo.Comprobante.Registros.Persistence;
using Module.Contribuyente.Perfiles.Persistence;

namespace Web.Api;

public static class BuilderExtensions
{
    public static void CheckMigrations(this WebApplication app)
    {
        app.CheckComprobanteMigrations()
            .CheckContribuyenteMigrations()
            .CheckErrorLogMigrations();
    }

    public static async Task SeedData(this WebApplication app)
    {
        await app.SeedContribuyentesAsync();
        await app.SeedConprobantesAsync();
    }
}