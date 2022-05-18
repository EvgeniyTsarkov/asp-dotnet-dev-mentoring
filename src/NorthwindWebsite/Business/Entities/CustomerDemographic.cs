namespace NorthwindWebsite.Entities
{
    public class CustomerDemographic
    {
        public string CustomerTypeId { get; set; }

        public string CustomerDesc { get; set; }

        public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
    }
}
