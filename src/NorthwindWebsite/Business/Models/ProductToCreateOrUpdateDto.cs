using NorthwindWebsite.Entities;
using NorthwindWebsite.Infrastructure.Entities;

namespace NorthwindWebsite.Business.Models
{
    public class ProductToCreateOrUpdateDto
    {
        public Product Product { get; set; }

        public List<Category> Categories { get; set; }

        public List<Supplier> Suppliers { get; set; }
    }
}
