using Global.Sources.ValueObjects.Abstractions;

namespace Global.Sources.ValueObjects.Values;

internal class IdentificationObject
    : ValueObject
{
    private readonly static int[] _allowLegth = new int[]{ 9, 11 };

    public string Value { get; init; }

    private IdentificationObject(string value)
        => Value = value;

    public static IdentificationObject Create(string value)
        => new(value);

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}