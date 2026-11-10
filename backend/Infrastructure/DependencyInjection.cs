using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration
                                   .GetConnectionString("DefaultConnection")
                               ?? throw new InvalidOperationException(
                                   "Database connection string is missing.");

        services.AddDbContext<CyberLabDbContext>(options =>
            options.UseNpgsql(connectionString));

        return services;
    }
}