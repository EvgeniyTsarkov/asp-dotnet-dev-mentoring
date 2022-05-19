using Microsoft.EntityFrameworkCore;
using NorthwindWebsite.Entities;
using NorthwindWebsite.Infrastructure.Entities;
using System.Reflection;

namespace NorthwindWebsite.Infrastructure
{
    public class NorthwindContext : DbContext
    {
        public NorthwindContext(DbContextOptions<NorthwindContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AlphabeticalListOfProduct> AlphabeticalListOfProducts { get; set; }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<CategorySalesFor1997> CategorySalesFor1997s { get; set; }

        public virtual DbSet<CurrentProductList> CurrentProductLists { get; set; }

        public virtual DbSet<Customer> Customers { get; set; }

        public virtual DbSet<CustomerAndSuppliersByCity> CustomerAndSuppliersByCities { get; set; }

        public virtual DbSet<CustomerDemographic> CustomerDemographics { get; set; }

        public virtual DbSet<Employee> Employees { get; set; }

        public virtual DbSet<Invoice> Invoices { get; set; }

        public virtual DbSet<Order> Orders { get; set; }

        public virtual DbSet<OrderDetail> OrderDetails { get; set; }

        public virtual DbSet<OrderDetailsExtended> OrderDetailsExtendeds { get; set; }

        public virtual DbSet<OrderSubtotal> OrderSubtotals { get; set; }

        public virtual DbSet<OrderSubtotal> OrdersQries { get; set; }

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<ProductSalesFor1997> ProductSalesFor1997s { get; set; }

        public virtual DbSet<ProductsAboveAveragePrice> ProductsAboveAveragePrices { get; set; }

        public virtual DbSet<ProductsByCategory> ProductsByCategories { get; set; }

        public virtual DbSet<QuarterlyOrder> QuarterlyOrders { get; set; }

        public virtual DbSet<Region> Regions { get; set; }

        public virtual DbSet<SalesByCategory> SalesByCategories { get; set; }

        public virtual DbSet<SalesTotalsByAmount> SalesTotalsByAmounts { get; set; }

        public virtual DbSet<Shipper> Shippers { get; set; }

        public virtual DbSet<SummaryOfSalesByQuarter> SummaryOfSalesByQuarters { get; set; }

        public virtual DbSet<SummaryOfSalesByYear> SummaryOfSalesByYears { get; set; }

        public virtual DbSet<Supplier> Suppliers { get; set; }

        public virtual DbSet<Territory> Territories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
