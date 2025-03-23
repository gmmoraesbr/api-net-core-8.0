namespace Base.Domain.ValueObjects;

public class Money
{
    public decimal Amount { get; }
    public string Currency { get; }

    protected Money() { } // For EF

    public Money(decimal amount, string currency = "USD")
    {
        if (amount < 0)
            throw new ArgumentException("Amount cannot be negative");

        Amount = amount;
        Currency = currency;
    }

    public override string ToString() => $"{Amount:C} {Currency}";
}