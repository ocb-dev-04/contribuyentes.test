using Microsoft.EntityFrameworkCore;

namespace Module.Internal.Error.Logs.Persistence.Context;

internal sealed class ErrorLogDbContext
    : DbContext
{
    public ErrorLogDbContext(DbContextOptions<ErrorLogDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ErrorLogDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}