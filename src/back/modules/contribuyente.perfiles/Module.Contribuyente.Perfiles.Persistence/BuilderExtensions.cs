using Bogus;
using Common.Sources.Abstractions;
using Global.Sources.ResultPattern;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Global.Sources.ValueObjects.Values;
using Microsoft.Extensions.DependencyInjection;
using Module.Contribuyente.Perfiles.Domain.Enums;
using Module.Contribuyente.Perfiles.Domain.Entities;
using Module.Contribuyente.Perfiles.Persistence.Context;

namespace Module.Contribuyente.Perfiles.Persistence;

public static class BuilderExtensions
{
    public static WebApplication CheckContribuyenteMigrations(this WebApplication app)
    {
        using IServiceScope scope = app.Services.CreateScope();
        using ContribuyenteDbContext context = scope.ServiceProvider.GetRequiredService<ContribuyenteDbContext>();

        bool canConnect = context.Database.CanConnect();
        if (!canConnect)
            throw new Exception("Can't connect to database");

        IEnumerable<string> pendingMigrations = context.Database.GetPendingMigrations();
        if (pendingMigrations.Any())
            context.Database.Migrate();

        return app;
    }

    public static async Task SeedContribuyentesAsync(this WebApplication app)
    {
        using IServiceScope scope = app.Services.CreateScope();
        using IUnitOfwork unitOfwork = scope.ServiceProvider.GetRequiredService<IUnitOfwork>();

        using CancellationTokenSource cts = new CancellationTokenSource(TimeSpan.FromSeconds(10));
        CancellationToken cancellationToken = cts.Token;

        bool exist = await unitOfwork.PerfilContribuyente.IsEmptyAsync(cancellationToken);
        if (exist) 
            return;

        IdentityType[] identityTypes = Enum.GetValues<IdentityType>();

        Faker<PerfilContribuyente> faker = new Faker<PerfilContribuyente>()
            .CustomInstantiator(f =>
                PerfilContribuyente.Create(
                    f.PickRandom(identityTypes),                 
                    StringObject.Create(f.Name.FullName()),      
                    StringObject.Create(f.Random.ReplaceNumbers("#########"))
                )
            );

        List<PerfilContribuyente> perfiles = faker.Generate(20);

        Task<Result>[] tasks = perfiles.Select(
            s => unitOfwork.PerfilContribuyente.CreateAsync(s, cancellationToken))
            .ToArray();
        await Task.WhenAll(tasks);

        await unitOfwork.PerfilContribuyente.CommitAsync(cancellationToken);
    }
}
