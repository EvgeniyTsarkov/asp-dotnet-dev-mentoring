using NorthwindWebsite.Business.Models;
using NorthwindWebsite.Entities;

namespace NorthwindWebsite.Tests.TestDataFactories
{
    public class ProductsTestDataProvider
    {
        private readonly IEnumerable<Product> _products = new List<Product>()
            {
                new()
                {
                    ProductId = 1, 
                    ProductName = "Chai", 
                    SupplierId = 1, 
                    CategoryId = 1, 
                    QuantityPerUnit = "10 boxes x 20 bags",
                    UnitPrice = (decimal?)18.00, 
                    UnitsInStock = 39, 
                    UnitsOnOrder = 0, 
                    ReorderLevel = 10, 
                    Discontinued = false
                },
                new()
                {
                    ProductId = 2, 
                    ProductName = "Chang", 
                    SupplierId = 2, 
                    CategoryId = 2, 
                    QuantityPerUnit = "24 -12 oz bottles",
                    UnitPrice = (decimal?)19.00, 
                    UnitsInStock = 17, 
                    UnitsOnOrder = 40, 
                    ReorderLevel = 25, 
                    Discontinued = false
                },
                new()
                {
                    ProductId = 3, 
                    ProductName = "Aniseed Syrup", 
                    SupplierId = 3, 
                    CategoryId = 3,
                    QuantityPerUnit = "12 - 550 ml bottles",
                    UnitPrice = (decimal?)10.00, 
                    UnitsInStock = 13, 
                    UnitsOnOrder = 70, 
                    ReorderLevel = 25, 
                    Discontinued = false
                },
            };

        public async Task<ProductsDto> GetProductsAsync()
        {
            var products = await Task.FromResult(_products);

            return new ProductsDto { Products = products.ToList() };
        }

        public async Task<ProductHandleDto> GetProductModelAsync(int id)
        {
            var product = await Task.FromResult(_products.SingleOrDefault(p => p.ProductId == id));

            return new ProductHandleDto { Product = product! };
        }
    }
}
