namespace NorthwindWebsite.Entities
{
    public class CustomerDemographic
    {
        public CustomerDemographic()
        {
            Customers = new List<Customer>();
        }

        public string CustomerTypeId { get; set; }

        public string CustomerDesc { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }
    }
}
