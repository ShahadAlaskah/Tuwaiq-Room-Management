using MediatR;

namespace Shared.Domain;

public class DomainEventRequest<TDomainEvent> : IRequest where TDomainEvent : IDomainEvent
{
    public DomainEventRequest(TDomainEvent domainEvent)
    {
        DomainEvent = domainEvent;
    }

    public TDomainEvent DomainEvent { get; }
}