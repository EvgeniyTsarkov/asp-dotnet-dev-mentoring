using NorthwindWebsite.Entities;
using NorthwindWebsite.Infrastructure.Entities;

namespace NorthwindWebsite.Business.Models
{
    public class ProductHandleDto
    {
        public Product Product { get; set; }

        public Dictionary<int, string> CategoryOptions { get; set; }

        public Dictionary<int, string> SupplierOptions { get; set; }
    }
}
