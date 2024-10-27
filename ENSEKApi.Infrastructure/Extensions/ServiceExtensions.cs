using ENSEKApi.Application;
using ENSEKApi.Domain.Interfaces;
using ENSEKApi.Infrastructure.SqlServer;
using ENSEKApi.Repositories;
using ENSEKApi.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

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
