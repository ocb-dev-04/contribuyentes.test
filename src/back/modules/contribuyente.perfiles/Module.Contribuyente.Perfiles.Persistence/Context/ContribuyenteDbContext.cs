using Microsoft.EntityFrameworkCore;

namespace Module.Contribuyente.Perfiles.Persistence.Context;

internal sealed class ContribuyenteDbContext
    : DbContext
{
    public ContribuyenteDbContext(DbContextOptions<ContribuyenteDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ContribuyenteDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}