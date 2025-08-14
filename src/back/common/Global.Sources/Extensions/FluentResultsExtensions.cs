using Global.Sources.ResultPattern;

namespace Global.Sources.Extensions;

public static class FluentResultsExtensions
{
    public static TReturnType Match<TReturnType, TValue>(
        this Result<TValue> result,
        Func<object, TReturnType> success,
        Func<Error, TReturnType> error)
            where TValue : notnull
            where TReturnType : notnull
                => result.IsSuccess
                    ? success(result.Value)
                    : error(result.Error);

    public static TReturnType Match<TReturnType>(
        this Result result,
        Func<TReturnType> success,
        Func<Error, TReturnType> error)
            where TReturnType : notnull
                => result.IsSuccess
                    ? success()
                    : error(result.Error);
}