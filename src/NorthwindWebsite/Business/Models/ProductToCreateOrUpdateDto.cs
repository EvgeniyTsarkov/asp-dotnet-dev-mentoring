using Microsoft.AspNetCore.Mvc.Rendering;
using NorthwindWebsite.Entities;

namespace NorthwindWebsite.Business.Models
{
    public class ProductToCreateOrUpdateDto
    {
        public Product Product { get; set; }

        public SelectList Categories { get; set; }

        public SelectList Suppliers { get; set; }
    }
}
