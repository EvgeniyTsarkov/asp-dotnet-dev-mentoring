using Microsoft.EntityFrameworkCore;
using NorthwindWebsite.Infrastructure;

namespace NorthwindWebsite.Configuration;

public static class DataAccessConfiguration
{
    public static void AddDbContextConfiguration(
        this IServiceCollection services,
        string сonnectionString)
    {
        services.AddDbContext<NorthwindContext>(options =>
            options.UseSqlServer(сonnectionString));
    }
}
