using MediatR;
using FluentValidation;
using Global.Sources.ErrorHandler;

namespace Presentation.Behaviors.Pipelines;

public sealed class ValidationPipelineBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
        where TResponse : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        ArgumentNullException.ThrowIfNull(validators, nameof(validators));

        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
            return await next();

        ValidationError[] errors = _validators
            .Select(async validator
                => await validator.ValidateAsync(request))
            .SelectMany(validationResult
                => validationResult.Result.Errors)
            .Where(validationFailure
                => validationFailure is not null)
            .Select(failure
                => new ValidationError(failure.PropertyName, failure.ErrorMessage))
            .Distinct()
            .ToArray();

        if (errors.Any())
            throw new Global.Sources.Exceptions.ValidationException(errors);

        return await next();
    }
}
