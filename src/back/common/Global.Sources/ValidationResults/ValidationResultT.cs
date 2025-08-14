using Global.Sources.ErrorHandler;

namespace Global.Sources.ValidationResults;

public class ValidationResult<TValue> : ValidationResult
{
    internal ValidationResult(ValidationError error)
        : base(error)
    {
    }
}