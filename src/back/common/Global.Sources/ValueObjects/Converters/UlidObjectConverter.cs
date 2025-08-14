using Global.Sources.ValueObjects.Values;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Global.Sources.ValueObjects.Converters;

public sealed class UlidObjectConverter
    : ValueConverter<UlidObject, string>
{
    public UlidObjectConverter()
        : base(
              ulidObject => ulidObject.Value,
              value => UlidObject.Create(value))
    {
    }
}
