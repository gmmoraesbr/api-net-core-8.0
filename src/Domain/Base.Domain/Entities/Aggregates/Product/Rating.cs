using Base.Domain.Common;

namespace Base.Domain.Entities.Aggregates.Product;

public class Rating : BaseEntity<int>
{
    public int ProductId { get; private set; }
    public decimal Rate { get; private set; }
    public int Count { get; private set; }

    protected Rating() { }

    private Rating(int productId, decimal rate, int count)
    {
        ProductId = productId;
        Rate = rate;
        Count = count;
    }

    public static Rating Create(int productId, decimal rate, int count)
    {
        if (rate < 0 || rate > 5)
            throw new ArgumentOutOfRangeException(nameof(rate), "Rate must be between 0 and 5.");

        if (count < 0)
            throw new ArgumentOutOfRangeException(nameof(count), "Count must be non-negative.");

        return new Rating(productId, rate, count);
    }

    public void Update(decimal rate, int count)
    {
        if (rate < 0 || rate > 5)
            throw new ArgumentOutOfRangeException(nameof(rate), "Rate must be between 0 and 5.");
        if (count < 0)
            throw new ArgumentOutOfRangeException(nameof(count), "Count must be non-negative.");

        Rate = rate;
        Count = count;
    }
}
