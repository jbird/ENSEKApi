using ENSEKApi.Infrastructure.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ENSEKApi.Infrastructure.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<EnsekDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("SqlConnectionString")));

        return services;
    }
}
