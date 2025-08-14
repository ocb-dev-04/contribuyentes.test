using Global.Sources.ValueObjects.Values;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Global.Sources.ValueObjects.Converters;

public sealed class DecimalObjectConverter
    : ValueConverter<DecimalObject, decimal>
{
    public DecimalObjectConverter()
        : base(
            (DecimalObject instance) => instance.Value,
            (decimal value) => DecimalObject.Create(value))
    {
    }
}