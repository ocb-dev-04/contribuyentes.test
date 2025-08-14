using Microsoft.AspNetCore.Http;

namespace Global.Sources.ResultPattern;

public record Error(int StatusCode, string Translation, string Description)
{
    public static Error None = new(0, string.Empty, string.Empty);
    public static Error NullValue = new(500, "nullValue", "Null value was provided");

    public static Error NotModified(string? translation = default, string? message = default)
        => new(StatusCodes.Status304NotModified, translation ?? string.Empty, message ?? string.Empty);

    public static Error BadRequest(string? translation = default, string? message = default)
        => new(StatusCodes.Status400BadRequest, translation ?? string.Empty, message ?? string.Empty);

    public static Error NotFound(string? translation = default, string? message = default)
        => new(StatusCodes.Status404NotFound, translation ?? string.Empty, message ?? string.Empty);

    public static Error TooManyRequest(string? translation = default, string? message = default)
        => new(StatusCodes.Status429TooManyRequests, translation ?? string.Empty, message ?? string.Empty);

    public static Error Unauthorized()
        => new(StatusCodes.Status401Unauthorized, string.Empty, string.Empty);
}