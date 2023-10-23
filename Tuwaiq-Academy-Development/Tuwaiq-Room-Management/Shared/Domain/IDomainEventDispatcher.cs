namespace Shared.Domain;

public interface IDomainEventDispatcher
{
    Task DispatchEvent(IDomainEvent devent);
    Task DispatchNotification(IDomainNotification devent);
}