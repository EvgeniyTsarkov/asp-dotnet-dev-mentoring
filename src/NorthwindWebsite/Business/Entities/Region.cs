namespace NorthwindWebsite.Entities
{
    public class Region
    {
        public Region()
        {
            Territories = new List<Territory>();
        }

        public int RegionId { get; set; }

        public string RegionDescription { get; set; }

        public virtual ICollection<Territory> Territories { get; set; }
    }
}
