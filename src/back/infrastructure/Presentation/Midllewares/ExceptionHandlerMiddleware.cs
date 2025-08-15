using Global.Sources.Events.Internal;
using Global.Sources.Exceptions;
using Global.Sources.ValueObjects.Values;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Presentation.Midllewares;

public sealed class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;

    public ExceptionHandlerMiddleware(
        RequestDelegate next,
        ILogger<ExceptionHandlerMiddleware> logger)
    {
        ArgumentNullException.ThrowIfNull(next, nameof(next));
        ArgumentNullException.ThrowIfNull(logger, nameof(logger));

        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (DbUpdateConcurrencyException dbUpdateEx)
        {
            _ = dbUpdateEx;
            context.Response.StatusCode = StatusCodes.Status409Conflict;
            await context.Response.WriteAsync(string.Empty);
        }
        catch (ValidationException exception)
        {
            await HandleBadRequest(exception, context);
        }
        catch (Exception ex)
        {
            await HandlerInternalServerError(ex, context);
        }
    }

    private async Task HandleBadRequest(ValidationException ex, HttpContext context)
    {
        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Type = "ValidationFailure",
            Title = "Validation error",
            Detail = "One or more validation errors has occurred"
        };

        if (ex.Errors.Any())
        {
            problemDetails.Extensions["errors"] = ex.Errors
                .Select(
                s =>
                {
                    string capitalized = string.Format("{0}{1}", char.ToUpper(s.ErrorMessage[0]), s.ErrorMessage.Substring(1));
                    return string.Format("{0}{1}", s.PropertyName.ToLower(), capitalized);
                })
                .ToArray();
        }

        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        await context.Response.WriteAsJsonAsync(problemDetails);
    }

    private async Task HandlerInternalServerError(Exception ex, HttpContext context)
    {
        _logger.LogError($"--> Some error ocurred: {0}", ex.InnerException);

        IServiceScope scope = context.RequestServices.CreateScope();
        IMediator mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

        string ipAddress = context.Connection.RemoteIpAddress?.ToString() ?? "Unknown";

        string path = context.Request.Path.HasValue
            ? context.Request.Path.Value
            : string.Empty;

        string method = context.Request.Method;

        Endpoint? endpoint = context.GetEndpoint();
        ControllerActionDescriptor? actionDescriptor = endpoint?.Metadata
            .GetMetadata<ControllerActionDescriptor>();

        string controllerName = actionDescriptor?.ControllerName ?? string.Empty;
        string actionName = actionDescriptor?.ActionName ?? string.Empty;

        CreateErrorLogInternalEvent errorEvent = new(
            StringObject.Create(ipAddress),
            StringObject.Create(path),
            StringObject.Create(controllerName),
            StringObject.Create(actionName),
            StringObject.Create(method),
            StringObject.Create(ex.InnerException is null ? string.Empty : ex.InnerException.ToString()),
            StringObject.Create(JsonSerializer.Serialize(ex.StackTrace)),
            DateTimeOffset.UtcNow);

        await mediator.Publish(errorEvent, context.RequestAborted);

        context.Response.StatusCode = 500;
        await context.Response.WriteAsync(string.Empty);
    }
}