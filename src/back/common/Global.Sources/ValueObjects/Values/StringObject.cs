using Global.Sources.ValueObjects.Abstractions;

namespace Global.Sources.ValueObjects.Values;

public sealed class StringObject
    : ValueObject
{
    public string Value { get; init; }

    private StringObject(string value)
        => Value = value;

    public static StringObject Create(string value)
        => new(value);

    public static StringObject CreateAsEmpty()
        => new(string.Empty);

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}