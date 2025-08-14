using Global.Sources.ValueObjects.Abstractions;

namespace Global.Sources.ValueObjects.Values;

public sealed class DecimalObject
    : ValueObject
{
    public decimal Value { get; private set; }

    private DecimalObject()
    {

    }

    private DecimalObject(decimal value)
        => Value = value;

    public static DecimalObject Create(decimal value)
        => new(value);

    public static DecimalObject Zero()
        => new(0);
    
    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}