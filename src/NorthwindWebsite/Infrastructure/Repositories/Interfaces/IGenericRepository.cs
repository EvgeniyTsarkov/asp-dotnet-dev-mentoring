namespace NorthwindWebsite.Infrastructure.Repositories.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<List<TEntity>> GetAll();
    }
}
