using Microsoft.EntityFrameworkCore;
using Global.Sources.ValueObjects.Converters;
using Module.Contribuyente.Perfiles.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Module.Contribuyente.Perfiles.Persistence.Configurations;

internal sealed class PerfilContribuyenteConfigurations
    : IEntityTypeConfiguration<PerfilContribuyente>
{
    public void Configure(EntityTypeBuilder<PerfilContribuyente> builder)
    {
        builder.Property(e => e.Id)
            .IsRequired()
            .HasMaxLength(26)
            .HasConversion<UlidObjectConverter>();

        builder.Property(e => e.Tipo)
            .IsRequired();

        builder.Property(e => e.Nombre)
            .IsRequired()
            .HasMaxLength(100)
            .HasConversion<StringObjectConverter>();

        builder.Property(e => e.RncCedula)
            .IsRequired()
            .HasMaxLength(11)
            .HasConversion<StringObjectConverter>();

        builder.Property(e => e.EstaActivo)
            .IsRequired()
            .HasConversion<BooleanObjectConverter>();

        builder.Property(e => e.CreadoEnUtc)
            .IsRequired();


        builder.HasKey(o => o.Id);
        builder.HasIndex(o => o.RncCedula).IsUnique();
        builder.HasIndex(i => new
        {
            i.Nombre,
            i.Tipo,
        }).HasMethod("Btree");

        builder.Metadata.SetSchema("contribuyentes");
    }
}
