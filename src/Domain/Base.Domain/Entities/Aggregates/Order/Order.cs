namespace Base.Domain.Entities.Aggregates.Order;
using Base.Domain.Common;
using Base.Domain.Events;
using Base.Domain.ValueObjects;

public class Order : BaseGuidEntity
{
    public Guid CustomerId { get; private set; }
    private readonly List<OrderItem> _items = new();
    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

    public Address ShippingAddress { get; private set; }

    protected Order() { } // For EF

    private Order(Guid customerId, Address shippingAddress)
    {
        CustomerId = customerId;
        ShippingAddress = shippingAddress;
    }

    public void AddItem(Guid productId, string productName, int quantity, Money unitPrice)
    {
        if (quantity <= 0)
            throw new InvalidOperationException("Quantity must be greater than zero.");

        var item = new OrderItem(productId, productName, quantity, unitPrice);
        _items.Add(item);
    }

    public void RemoveItem(Guid productId)
    {
        var item = _items.FirstOrDefault(i => i.ProductId == productId);
        if (item is not null)
        {
            _items.Remove(item);
        }
    }

    public Money GetTotalAmount()
    {
        var total = _items.Sum(i => i.Total.Amount); // <- Corrigido aqui
        var currency = _items.FirstOrDefault()?.Total.Currency ?? "USD";
        return new Money(total, currency);
    }

    public static Order Create(Guid customerId, Address address)
    {
        var order = new Order(customerId, address);

        order.AddDomainEvent(new OrderCreatedEvent(order));

        return order;
    }

}