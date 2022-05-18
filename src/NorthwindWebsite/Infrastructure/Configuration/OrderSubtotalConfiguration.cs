using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NorthwindWebsite.Entities;

namespace NorthwindWebsite.Infrastructure.Configuration
{
    public class OrderSubtotalConfiguration : IEntityTypeConfiguration<OrdersSubtotal>
    {
        public void Configure(EntityTypeBuilder<OrdersSubtotal> builder)
        {
            builder.HasNoKey();

            builder.ToView("Order Subtotals");

            builder.Property(e => e.OrderId).HasColumnName("OrderID");

            builder.Property(e => e.Subtotal).HasColumnType("money");
        }
    }
}
