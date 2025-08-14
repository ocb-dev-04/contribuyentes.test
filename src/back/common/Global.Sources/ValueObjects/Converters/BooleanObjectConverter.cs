using Global.Sources.ValueObjects.Values;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Global.Sources.ValueObjects.Converters;

public sealed class BooleanObjectConverter
    : ValueConverter<BooleanObject, bool>
{
    public BooleanObjectConverter()
        : base(
            (BooleanObject instance) => instance.Value,
            (bool value) => BooleanObject.Create(value))
    {
    }
}