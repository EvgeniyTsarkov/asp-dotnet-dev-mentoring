﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NorthwindWebsite.Entities;

namespace NorthwindWebsite.Infrastructure.Configuration
{
    public class SalesByCategoryConfiguration : IEntityTypeConfiguration<SalesByCategory>
    {
        public void Configure(EntityTypeBuilder<SalesByCategory> builder)
        {
            builder.HasNoKey();

            builder.ToView("Sales by Category");

            builder.Property(e => e.CategoryId).HasColumnName("CategoryID");

            builder.Property(e => e.CategoryName).HasMaxLength(15);

            builder.Property(e => e.ProductName).HasMaxLength(40);

            builder.Property(e => e.ProductSales).HasColumnType("money");
        }
    }
}
