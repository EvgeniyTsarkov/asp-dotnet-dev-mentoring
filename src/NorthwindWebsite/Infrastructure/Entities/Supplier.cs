using System.ComponentModel.DataAnnotations;

namespace NorthwindWebsite.Entities;

public class Supplier
{
    public int SupplierId { get; set; }

    [StringLength(50)]
    [Display(Name = "Supplier")]
    public string CompanyName { get; set; }

    public string ContactName { get; set; }

    public string ContactTitle { get; set; }

    public string Address { get; set; }

    public string City { get; set; }

    //This nullable is required for normal EF functioning
    public string? Region { get; set; }

    public string PostalCode { get; set; }

    public string Country { get; set; }

    public string Phone { get; set; }

    //This nullable is required for normal EF functioning
    public string? Fax { get; set; }

    //This nullable is required for normal EF functioning
    public string? HomePage { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
