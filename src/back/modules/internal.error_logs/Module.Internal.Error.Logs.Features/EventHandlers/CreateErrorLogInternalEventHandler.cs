using MediatR;
using Microsoft.Extensions.Logging;
using Global.Sources.Events.Internal;
using Microsoft.Extensions.DependencyInjection;
using Module.Internal.Error.Logs.Domain.Entities;
using Module.Internal.Error.Logs.Domain.Abstractions.Repositories;

namespace Module.Internal.Error.Logs.Features.EventHandlers;

internal sealed class CreateErrorLogInternalEventHandler
    : INotificationHandler<CreateErrorLogInternalEvent>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<CreateErrorLogInternalEventHandler> _logger;

    /// <summary>
    /// <see cref="CreateErrorLogInternalEventHandler"/> public constructor
    /// </summary>
    /// <param name="serviceScopeFactory"></param>
    /// <param name="logger"></param>
    public CreateErrorLogInternalEventHandler(
        IServiceScopeFactory serviceScopeFactory,
        ILogger<CreateErrorLogInternalEventHandler> logger)
    {
        _serviceScopeFactory = serviceScopeFactory ?? throw new ArgumentNullException(nameof(serviceScopeFactory));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger)); ;
    }

    public async Task Handle(CreateErrorLogInternalEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("--> Executing {EventHandlerName} at: {CurrentDateTime}", this.GetType().Name, DateTimeOffset.UtcNow);

        using IServiceScope scope = _serviceScopeFactory.CreateScope();
        IErrorLogRepository repository = scope.ServiceProvider.GetRequiredService<IErrorLogRepository>();

        bool exist = await repository.ExistAsync(
            e => e.InnerException.Equals(notification.InnerException) &&
                 e.Path.Equals(notification.Path) &&
                 e.IpAddress.Equals(notification.IpAddress));
        if (exist)
            return;

        await repository.CreateAsync(ErrorLog.MapFromEvent(notification), cancellationToken);
    }
}
