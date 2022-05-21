using NorthwindWebsite.Entities;

namespace NorthwindWebsite.Business.Models
{
    public class ProductsListDto
    {
        public List<Product> Products { get; set; }

        public int MaximumProductsOnPage { get; set; }
    }
}
