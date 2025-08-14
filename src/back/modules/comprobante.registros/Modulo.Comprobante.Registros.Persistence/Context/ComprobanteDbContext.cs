using Microsoft.EntityFrameworkCore;

namespace Modulo.Comprobante.Registros.Persistence.Context;

internal sealed class ComprobanteDbContext
    : DbContext
{
    public ComprobanteDbContext(DbContextOptions<ComprobanteDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ComprobanteDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}