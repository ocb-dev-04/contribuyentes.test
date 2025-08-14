using Global.Sources.ValueObjects.Values;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Global.Sources.ValueObjects.Converters;

public sealed class StringObjectConverter
    : ValueConverter<StringObject, string>
{
    public StringObjectConverter()
        : base(
            (StringObject instance) => instance.Value,
            (string value) => StringObject.Create(value))
    {
    }
}