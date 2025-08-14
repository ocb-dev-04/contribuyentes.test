using Global.Sources.ErrorHandler;

namespace Global.Sources.ValidationResults;

public class ValidationResult
{
    internal ValidationError Error { get; }

    internal ValidationResult(ValidationError error)
        => Error = error;

    internal static ValidationResult<TValue> Success<TValue>(TValue value)
        => new(ValidationError.None);

    internal static ValidationResult<TValue> Failure<TValue>(ValidationError error)
        => new(error);

    internal static ValidationResult<TValue> Failure<TValue>()
        => new(ValidationError.None);

    internal static ValidationResult<TValue> Create<TValue>(TValue? value) =>
        value is not null ? Success(value) : Failure<TValue>(ValidationError.NullValue);
}
