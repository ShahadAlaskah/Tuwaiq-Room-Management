using MediatR;

namespace Shared.Domain;

public class MediatrDomainEventDispatcher : IDomainEventDispatcher
{
    // private readonly ILoggerAdapter<MediatrDomainEventDispatcher> _log;
    private readonly IMediator _mediator;

    public MediatrDomainEventDispatcher(IMediator mediator)
    {
        _mediator = mediator;
        // _log = log ?? throw new ArgumentNullException(nameof(log));
    }

    public async Task DispatchEvent(IDomainEvent devent)
    {
        var domainEventNotification = _createDomainEvent(devent);
        // _log.LogDebug("Dispatching Domain Event as MediatR event.  EventType: {eventType}", devent.GetType());

        await _mediator.Send(domainEventNotification);
    }

    public async Task DispatchNotification(IDomainNotification devent)
    {
        var domainEventNotification = _createDomainNotification(devent);
        // _log.LogDebug("Dispatching Domain notification as MediatR notification.  EventType: {eventType}",devent.GetType());

        await _mediator.Publish(domainEventNotification);
    }

    private IRequest _createDomainEvent(IDomainEvent domainEvent)
    {
        var genericDispatcherType = typeof(DomainEventRequest<>).MakeGenericType(domainEvent.GetType());
        return (IRequest)Activator.CreateInstance(genericDispatcherType, domainEvent)!;
    }

    private INotification _createDomainNotification(IDomainNotification domainEvent)
    {
        var genericDispatcherType = typeof(DomainEventNotification<>).MakeGenericType(domainEvent.GetType());
        return (INotification)Activator.CreateInstance(genericDispatcherType, domainEvent)!;
    }
}