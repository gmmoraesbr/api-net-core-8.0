using Base.Domain.Entities.Aggregates.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Base.Infrastructure.Data.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey("OrderId", nameof(OrderItem.ProductId));
        builder.OwnsOne(i => i.UnitPrice);
    }
}