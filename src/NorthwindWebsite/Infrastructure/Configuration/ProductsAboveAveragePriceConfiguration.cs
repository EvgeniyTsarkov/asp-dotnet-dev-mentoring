using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NorthwindWebsite.Entities;

namespace NorthwindWebsite.Infrastructure.Configuration
{
    public class ProductsAboveAveragePriceConfiguration : IEntityTypeConfiguration<ProductsAboveAveragePrice>
    {
        public void Configure(EntityTypeBuilder<ProductsAboveAveragePrice> builder)
        {
            builder.HasNoKey();

            builder.ToView("Products Above Average Price");

            builder.Property(e => e.ProductName).HasMaxLength(40);

            builder.Property(e => e.UnitPrice).HasColumnType("money");
        }
    }
}
