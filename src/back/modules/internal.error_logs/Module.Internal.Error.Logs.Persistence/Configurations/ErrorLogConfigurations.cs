using Microsoft.EntityFrameworkCore;
using Global.Sources.ValueObjects.Converters;
using Module.Internal.Error.Logs.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Module.Internal.Error.Logs.Persistence.Configurations;

internal sealed class ErrorLogConfigurations
    : IEntityTypeConfiguration<ErrorLog>
{
    public void Configure(EntityTypeBuilder<ErrorLog> builder)
    {
        builder.Property(e => e.Id)
            .IsRequired()
            .HasMaxLength(26)
            .HasConversion<UlidObjectConverter>();

        builder.Property(e => e.IpAddress)
            .IsRequired()
            .HasMaxLength(50)
            .HasConversion<StringObjectConverter>();

        builder.Property(e => e.Path)
            .IsRequired()
            .HasMaxLength(500)
            .HasConversion<StringObjectConverter>();

        builder.Property(e => e.Controller)
            .IsRequired()
            .HasMaxLength(150)
            .HasConversion<StringObjectConverter>();

        builder.Property(e => e.Action)
            .IsRequired()
            .HasMaxLength(150)
            .HasConversion<StringObjectConverter>();

        builder.Property(e => e.Method)
            .IsRequired()
            .HasMaxLength(10)
            .HasConversion<StringObjectConverter>();

        builder.Property(e => e.InnerException)
            .IsRequired()
            .HasConversion<StringObjectConverter>();

        builder.Property(e => e.StackTrace)
            .IsRequired()
            .HasConversion<StringObjectConverter>();

        builder.Property(e => e.CreatedOnUtc)
            .IsRequired();


        builder.HasKey(o => o.Id);
        builder.HasIndex(i => new
        {
            i.Method,
            i.Action,
            i.Controller,
            i.Path,
            i.IpAddress
        }).HasMethod("Btree");

        builder.Metadata.SetSchema("internal");
    }
}
