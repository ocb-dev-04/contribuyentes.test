using Global.Sources.ValueObjects.Abstractions;

namespace Global.Sources.ValueObjects.Values;

public sealed class UlidObject
    : ValueObject
{
    private static readonly string _invalidToParse = "invalidIdToParse";

    public string Value { get; init; }

    public Ulid AsUlid 
        => Ulid.Parse(Value);

    private UlidObject()
    {

    }

    private UlidObject(string value)
        => Value = value;

    public static UlidObject Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ApplicationException(_invalidToParse);

        if (!Ulid.TryParse(value, out Ulid validUlid))
            throw new ApplicationException(_invalidToParse);

        return new UlidObject(validUlid.ToString());
    }

    public static UlidObject Create(byte[] value)
    {
        if (value is null || !value.Length.Equals(16))
            throw new ApplicationException(_invalidToParse);

        if (!Ulid.TryParse(value, out Ulid validUlid))
            throw new ApplicationException(_invalidToParse);

        return new UlidObject(validUlid.ToString());
    }

    public static UlidObject New()
        => new(Ulid.NewUlid().ToString());

    public static bool operator >(UlidObject a, UlidObject b)
        => string.Compare(a.Value, b.Value, StringComparison.Ordinal) > 0;

    public static bool operator <(UlidObject a, UlidObject b)
        => string.Compare(a.Value, b.Value, StringComparison.Ordinal) < 0;

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}