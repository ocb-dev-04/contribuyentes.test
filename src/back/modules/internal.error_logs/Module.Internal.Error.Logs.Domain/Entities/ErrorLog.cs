using Global.Sources.Events.Internal;
using Global.Sources.ValueObjects.Values;

namespace Module.Internal.Error.Logs.Domain.Entities;

public sealed class ErrorLog
{
    public UlidObject Id { get; init; }

    public StringObject IpAddress { get; private set; }
    public StringObject Path { get; private set; }

    public StringObject Controller { get; private set; }
    public StringObject Action { get; private set; }
    public StringObject Method { get; private set; }

    public StringObject InnerException { get; private set; }
    public StringObject StackTrace { get; private set; }

    public DateTimeOffset CreatedOnUtc { get; private set; }

    internal ErrorLog()
    {

    }

    private ErrorLog(
        StringObject ip,
        StringObject path,
        StringObject controller,
        StringObject action,
        StringObject method,
        StringObject innerException,
        StringObject stackTrace,
        DateTimeOffset createdOnUtc)
    {
        this.Id = UlidObject.New();
        this.IpAddress = ip;
        this.Path = path;
        this.Controller = controller;
        this.Action = action;
        this.Method = method;
        this.InnerException = innerException;
        this.StackTrace = stackTrace;
        this.CreatedOnUtc = createdOnUtc;
    }

    public static ErrorLog MapFromEvent(CreateErrorLogInternalEvent integrationEvent)
        => new(
            integrationEvent.IpAddress,
            integrationEvent.Path,
            integrationEvent.Controller,
            integrationEvent.Action,
            integrationEvent.Method,
            integrationEvent.InnerException,
            integrationEvent.StackTrace,
            integrationEvent.CreatedOnUtc);
}
