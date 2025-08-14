using Global.Sources.ErrorHandler;

namespace Global.Sources.Abstractions.Validation;

public interface IValidationResult
{
    public static readonly ValidationError ValidationErrors = new(
        "ValidationError",
        "A validation problem ocurred");

    ValidationError[] Errors { get; }
}