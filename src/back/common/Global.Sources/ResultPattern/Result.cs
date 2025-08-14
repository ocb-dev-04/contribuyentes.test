using System.Diagnostics.CodeAnalysis;

namespace Global.Sources.ResultPattern;

public class Result
{
    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    public Error Error { get; }

    protected Result(bool isSuccess, Error error)
    {
        if (isSuccess && !error.Equals(Error.None))
            throw new InvalidOperationException();

        if (!isSuccess && error.Equals(Error.None))
            throw new InvalidOperationException();

        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Success()
        => new(true, Error.None);

    public static Result Failure(Error error)
        => new(false, error);

    public static Result Failure()
        => new(false, Error.None);

    public static Result<TValue> Success<TValue>(TValue value)
        => new(value, true, Error.None);

    public static Result<TValue> Failure<TValue>(Error error)
        => new(default, false, error);

    public static Result<TValue> Failure<TValue>()
        => new(default, false, Error.None);

    public static Result<TValue> Create<TValue>(TValue? value)
        => value is not null
            ? Success(value)
            : Failure<TValue>(Error.NullValue);
}

public sealed class Result<TValue> : Result
{
    private readonly TValue? _value;

    public Result(TValue? value, bool isSuccess, Error error)
        : base(isSuccess, error)
        => _value = value;

    [NotNull]
    public TValue Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("The value of a failure result can not be accessed.");

    public static implicit operator Result<TValue>(TValue? value) => Create(value);
}