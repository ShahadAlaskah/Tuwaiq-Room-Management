using System.Collections.Concurrent;

namespace Shared.Domain;

public interface IEntity : IBaseEntity
{
    IProducerConsumerCollection<IDomainEvent> DomainEvents { get; }
    IProducerConsumerCollection<IDomainNotification> DomainNotifications { get; }
}