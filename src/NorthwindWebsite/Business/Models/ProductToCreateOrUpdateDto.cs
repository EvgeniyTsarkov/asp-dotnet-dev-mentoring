using Microsoft.AspNetCore.Mvc.Rendering;
using NorthwindWebsite.Entities;

namespace NorthwindWebsite.Business.Models
{
    public class ProductToCreateOrUpdateDto
    {
        public Product Product { get; set; }

        public SelectList CategoryOptions { get; set; }

        public SelectList SupplierOptions { get; set; }
    }
}
