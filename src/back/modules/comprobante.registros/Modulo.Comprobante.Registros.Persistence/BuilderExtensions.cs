using Bogus;
using Common.Sources.Abstractions;
using Global.Sources.ResultPattern;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Global.Sources.ValueObjects.Values;
using Microsoft.Extensions.DependencyInjection;
using Modulo.Comprobante.Registros.Domain.Entities;
using Modulo.Comprobante.Registros.Persistence.Context;

namespace Modulo.Comprobante.Registros.Persistence;

public static class BuilderExtensions
{
    public static WebApplication CheckComprobanteMigrations(this WebApplication app)
    {
        using IServiceScope scope = app.Services.CreateScope();
        using ComprobanteDbContext context = scope.ServiceProvider.GetRequiredService<ComprobanteDbContext>();

        bool canConnect = context.Database.CanConnect();
        if (!canConnect)
            throw new Exception("Can't connect to database");

        IEnumerable<string> pendingMigrations = context.Database.GetPendingMigrations();
        if (pendingMigrations.Any())
            context.Database.Migrate();

        return app;
    }

    public static async Task SeedConprobantesAsync(this WebApplication app)
    {
        using IServiceScope scope = app.Services.CreateScope();
        using IUnitOfwork unitOfwork = scope.ServiceProvider.GetRequiredService<IUnitOfwork>();

        using CancellationTokenSource cts = new CancellationTokenSource(TimeSpan.FromSeconds(10));
        CancellationToken cancellationToken = cts.Token;

        bool exist = await unitOfwork.ComprobanteRegistro.IsEmptyAsync(cancellationToken);
        if (exist)
            return;

        var perfiles = await unitOfwork.PerfilContribuyente.CollectionAsync(1, cancellationToken: cancellationToken);
        if (perfiles.TotalItems.Equals(0))
            return;

        UlidObject[] idsCollection = perfiles.Data.Select(x => x.Id).ToArray();

        Task<Result>[] tasks = idsCollection.Select(
            async s =>
            {
                Faker<ComprobanteRegistro> faker = new Faker<ComprobanteRegistro>()
                    .CustomInstantiator(ff =>
                        ComprobanteRegistro.Create(
                            UlidObject.New(),
                            s,
                            StringObject.Create(ff.Random.Replace("B##########")),
                            DecimalObject.Create(ff.Random.Decimal(1000m, 50000m))
                        )
                    );
                ComprobanteRegistro[] registros = faker.Generate(100).ToArray();
                return await unitOfwork.ComprobanteRegistro.CreateRangeAsync(registros, cancellationToken);
            }).ToArray();

        await Task.WhenAll(tasks);
        await unitOfwork.ComprobanteRegistro.CommitAsync(cancellationToken);
    }
}
