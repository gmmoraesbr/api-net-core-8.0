using Base.Domain.Entities.Aggregates.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Base.Infrastructure.Data.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.Id);
        builder.OwnsOne(o => o.ShippingAddress);
        //builder.HasMany(typeof(OrderItem), "_items").WithOne().HasForeignKey("OrderId");

        //builder
        //    .HasMany(typeof(OrderItem), "_items") // diz ao EF que a coleção está em um campo
        //    .WithOne()                            // sem navegação inversa
        //    .HasForeignKey("OrderId")             // FK explícita
        //    .OnDelete(DeleteBehavior.Cascade);

        //builder.Metadata
        //    .FindNavigation(nameof(Order.Items))!
        //    .SetPropertyAccessMode(PropertyAccessMode.Field);

        builder
            .HasMany(o => o.Items)
            .WithOne()
            .HasForeignKey("OrderId");

        builder.Navigation(o => o.Items)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}