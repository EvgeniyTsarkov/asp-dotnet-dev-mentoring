namespace NorthwindWebsite.Infrastructure.Repositories.Implementation
{
    public abstract class BaseRepository
    {
        protected NorthwindContext _context;

        protected BaseRepository(NorthwindContext northwindContext)
        {
            _context = northwindContext;
        }
    }
}
