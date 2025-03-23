using Base.Domain.Common;
using Base.Domain.Entities.Aggregates.Product;

namespace Base.Domain.Events;


public class ProductCreatedEvent : IDomainEvent
{
    public Product Product { get; }
    public DateTime OccurredOn { get; } = DateTime.UtcNow;

    public ProductCreatedEvent(Product product)
    {
        Product = product;
    }
}

