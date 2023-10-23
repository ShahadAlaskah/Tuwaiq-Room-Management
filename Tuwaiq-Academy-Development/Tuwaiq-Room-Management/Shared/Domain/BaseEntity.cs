using System.Collections.Concurrent;

namespace Shared.Domain;

public abstract class BaseEntity : IEntity
{
    private readonly ConcurrentQueue<IDomainEvent> _domainEvents = new();

    private readonly ConcurrentQueue<IDomainNotification> _domainNotifications = new();
    public IProducerConsumerCollection<IDomainEvent> DomainEvents => _domainEvents;
    public IProducerConsumerCollection<IDomainNotification> DomainNotifications => _domainNotifications;

    protected void PublishEvent(IDomainEvent @event)
    {
        _domainEvents.Enqueue(@event);
    }

    protected void PublishNotification(IDomainNotification @event)
    {
        _domainNotifications.Enqueue(@event);
    }
}