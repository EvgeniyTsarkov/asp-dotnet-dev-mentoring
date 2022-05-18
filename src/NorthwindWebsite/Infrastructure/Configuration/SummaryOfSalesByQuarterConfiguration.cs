using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NorthwindWebsite.Entities;

namespace NorthwindWebsite.Infrastructure.Configuration
{
    public class SummaryOfSalesByQuarterConfiguration : IEntityTypeConfiguration<SummaryOfSalesByQuarter>
    {
        public void Configure(EntityTypeBuilder<SummaryOfSalesByQuarter> builder)
        {
            builder.HasNoKey();

            builder.ToView("Summary of Sales by Quarter");

            builder.Property(e => e.OrderId).HasColumnName("OrderID");

            builder.Property(e => e.ShippedDate).HasColumnType("datetime");

            builder.Property(e => e.Subtotal).HasColumnType("money");
        }
    }
}
