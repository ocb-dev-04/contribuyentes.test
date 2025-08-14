using MediatR;
using Global.Sources.ValueObjects.Values;

namespace Global.Sources.Events.Internal;

public sealed record CreateErrorLogInternalEvent(
    StringObject IpAddress,
    StringObject Path,
    StringObject Controller,
    StringObject Action,
    StringObject Method,
    StringObject InnerException,
    StringObject StackTrace,
    DateTimeOffset CreatedOnUtc) : INotification;