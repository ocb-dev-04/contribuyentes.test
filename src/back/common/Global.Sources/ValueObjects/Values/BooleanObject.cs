using Global.Sources.ValueObjects.Abstractions;

namespace Global.Sources.ValueObjects.Values;

public sealed class BooleanObject
    : ValueObject
{
    public bool Value { get; private set; }

    private BooleanObject()
    {

    }

    private BooleanObject(bool value)
        => Value = value;

    public static BooleanObject Create(bool value)
        => new BooleanObject(value);

    public static BooleanObject CreateAsTrue()
        => new BooleanObject(true);

    public static BooleanObject CreateAsFalse()
        => new BooleanObject(false);

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}