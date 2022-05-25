using NorthwindWebsite.Entities;
using System.ComponentModel.DataAnnotations;

namespace NorthwindWebsite.Infrastructure.Entities;

public class Category
{
    public int CategoryId { get; set; }

    [StringLength(50)]
    [Display(Name = "Category")]
    public string CategoryName { get; set; }

    public string Description { get; set; }

    public byte[]? Picture { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
