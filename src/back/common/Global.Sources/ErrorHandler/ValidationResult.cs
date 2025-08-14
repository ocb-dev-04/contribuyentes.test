using Global.Sources.Abstractions.Validation;

namespace Global.Sources.ErrorHandler;

public class ValidationResult : ValidationResults.ValidationResult, IValidationResult
{
    private ValidationResult(ValidationError[] errors)
        : base(IValidationResult.ValidationErrors)
        => Errors = errors;

    public ValidationError[] Errors { get; }

    public static ValidationResult WithErrors(ValidationError[] errors)
        => new(errors);
}