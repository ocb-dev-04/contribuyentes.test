namespace Global.Sources.ErrorHandler;

public sealed record ValidationError(string PropertyName, string ErrorMessage)
{
    public static ValidationError None = new(string.Empty, string.Empty);
    public static ValidationError NullValue = new("nullValue", "Null value was provided");
}