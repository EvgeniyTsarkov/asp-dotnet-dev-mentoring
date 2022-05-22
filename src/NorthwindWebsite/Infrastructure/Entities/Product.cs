using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using NorthwindWebsite.Infrastructure.Entities;
using System.ComponentModel.DataAnnotations;

namespace NorthwindWebsite.Entities;

public class Product
{
    public int ProductId { get; set; }

    [Required]
    [StringLength(50)]
    [Display(Name = "Product Name")]
    public string ProductName { get; set; }

    [Required(ErrorMessage = "Please select the supplier")]
    public int? SupplierId { get; set; }

    [Required(ErrorMessage = "Please select the category")]
    public int? CategoryId { get; set; }

    [Required]
    [StringLength(25)]
    [Display(Name = "Quantity Per Unit")]
    public string QuantityPerUnit { get; set; }

    [Required]
    [Range(0, 9999.99)]
    [RegularExpression("^(?:0|[1-9][0-9]*)\\.[0-9]{2}",
    ErrorMessage = "Wrong number format, please use standard price formats")]
    [DisplayFormat(DataFormatString = "{0:F}", ApplyFormatInEditMode = true)]
    [Display(Name = "Unit Price")]
    public decimal? UnitPrice { get; set; }

    [Required]
    [Range(0, 999)]
    [Display(Name = "Units In Stock")]
    public short? UnitsInStock { get; set; }

    [Required]
    [Range(0, 999)]
    [Display(Name = "Units In Order")]
    public short? UnitsOnOrder { get; set; }

    [Required]
    [Range(0, 99)]
    [Display(Name = "Reorder Level")]
    public short? ReorderLevel { get; set; }

    [ValidateNever]
    [Display(Name = "Discontinued")]
    public bool Discontinued { get; set; }

    public virtual Category? Category { get; set; }

    public virtual Supplier? Supplier { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
