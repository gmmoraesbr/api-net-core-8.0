namespace Base.Domain.Entities.Aggregates.Order;

using Base.Domain.Common;
using Base.Domain.ValueObjects;

public class OrderItem : BaseGuidEntity
{
    public Guid ProductId { get; private set; }
    public string ProductName { get; private set; }
    public int Quantity { get; private set; }
    public Money UnitPrice { get; private set; }
    public Money Total => new Money(UnitPrice.Amount * Quantity, UnitPrice.Currency);

    protected OrderItem() { } // For EF

    public OrderItem(Guid productId, string productName, int quantity, Money unitPrice)
    {
        if (string.IsNullOrWhiteSpace(productName))
            throw new ArgumentException("Invalid product name.");

        ProductId = productId;
        ProductName = productName;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }
}