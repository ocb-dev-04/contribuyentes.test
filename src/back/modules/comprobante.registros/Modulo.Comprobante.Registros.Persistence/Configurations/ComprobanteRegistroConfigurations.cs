using Microsoft.EntityFrameworkCore;
using Global.Sources.ValueObjects.Converters;
using Modulo.Comprobante.Registros.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Modulo.Comprobante.Registros.Persistence.Configurations;

internal sealed class ComprobanteRegistroConfigurations
    : IEntityTypeConfiguration<ComprobanteRegistro>
{
    public void Configure(EntityTypeBuilder<ComprobanteRegistro> builder)
    {
        builder.Property(e => e.Id)
            .IsRequired()
            .HasMaxLength(26)
            .HasConversion<UlidObjectConverter>();

        builder.Property(e => e.PerfilContribuyenteId)
            .IsRequired()
            .HasMaxLength(26)
            .HasConversion<UlidObjectConverter>();

        builder.Property(e => e.NCF)
            .IsRequired()
            .HasMaxLength(11)
            .HasConversion<StringObjectConverter>();

        builder.Property(e => e.Monto)
            .IsRequired()
            .HasPrecision(16, 2)
            .HasConversion<DecimalObjectConverter>();

        builder.Property(e => e.Itbis18)
            .IsRequired()
            .HasPrecision(16, 2)
            .HasConversion<DecimalObjectConverter>();

        builder.Property(e => e.CreadoEnUtc)
            .IsRequired();


        builder.HasKey(o => o.Id);
        builder.HasIndex(i => new
        {
            i.PerfilContribuyenteId,
            i.NCF,
            i.Monto
        }).HasMethod("Btree");

        builder.Metadata.SetSchema("comprobantes");
    }
}
