using MediatR;

namespace Shared.Domain;

public class DomainEventNotification<TDomainEvent> : INotification where TDomainEvent : IDomainNotification
{
    public DomainEventNotification(TDomainEvent domainEvent)
    {
        DomainEvent = domainEvent;
    }

    public TDomainEvent DomainEvent { get; }
}