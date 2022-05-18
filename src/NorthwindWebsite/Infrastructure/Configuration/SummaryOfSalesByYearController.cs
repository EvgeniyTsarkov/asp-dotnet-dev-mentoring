using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NorthwindWebsite.Entities;

namespace NorthwindWebsite.Infrastructure.Configuration
{
    public class SummaryOfSalesByYearController : IEntityTypeConfiguration<SummaryOfSalesByYear>
    {
        public void Configure(EntityTypeBuilder<SummaryOfSalesByYear> builder)
        {
            builder.HasNoKey();

            builder.ToView("Summary of Sales by Year");

            builder.Property(e => e.OrderId).HasColumnName("OrderID");

            builder.Property(e => e.ShippedDate).HasColumnType("datetime");

            builder.Property(e => e.Subtotal).HasColumnType("money");
        }
    }
}
