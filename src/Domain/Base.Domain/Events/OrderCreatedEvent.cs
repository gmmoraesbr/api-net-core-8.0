using Base.Domain.Common;
using Base.Domain.Entities.Aggregates.Order;

namespace Base.Domain.Events;


public class OrderCreatedEvent : IDomainEvent
{
    public Order Order { get; }
    public DateTime OccurredOn { get; } = DateTime.UtcNow;

    public OrderCreatedEvent(Order order)
    {
        Order = order;
    }
}

