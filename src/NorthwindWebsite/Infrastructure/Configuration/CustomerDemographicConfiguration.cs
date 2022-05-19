using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NorthwindWebsite.Entities;

namespace NorthwindWebsite.Infrastructure.Configuration
{
    public class CustomerDemographicConfiguration : IEntityTypeConfiguration<CustomerDemographic>
    {
        public void Configure(EntityTypeBuilder<CustomerDemographic> builder)
        {
            builder.HasKey(e => e.CustomerTypeId)
                .IsClustered(false);

            builder.Property(e => e.CustomerTypeId)
                .HasMaxLength(10)
                .HasColumnName("CustomerTypeID")
                .IsFixedLength();

            builder.Property(e => e.CustomerDesc).HasColumnType("ntext");
        }
    }
}
