using Base.Domain.Entities.Aggregates.Product;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Infrastructure.Data.Configurations;

public class RatingConfiguration : IEntityTypeConfiguration<Rating>
{
    public void Configure(EntityTypeBuilder<Rating> builder)
    {
        builder.ToTable("Ratings");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.ProductId)
            .IsRequired();

        builder.Property(r => r.Rate)
            .IsRequired()
            .HasColumnType("decimal(3,2)");

        builder.Property(r => r.Count)
            .IsRequired();
    }
}