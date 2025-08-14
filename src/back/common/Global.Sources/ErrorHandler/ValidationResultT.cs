using Global.Sources.Abstractions.Validation;

namespace Global.Sources.ErrorHandler;

public sealed class ValidationResult<TValue> : ValidationResults.ValidationResult<TValue>, IValidationResult
{
    public ValidationResult(ValidationError[] errors)
        : base(IValidationResult.ValidationErrors)
        => Errors = errors;

    public ValidationError[] Errors { get; }

    public static ValidationResult<TValue> WithErrors(ValidationError[] errors)
        => new(errors);
}