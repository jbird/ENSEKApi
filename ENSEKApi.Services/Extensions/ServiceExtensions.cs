using ENSEKApi.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ENSEKApi.Services.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddTransient<IMeterReadingService, MeterReadingService>();
        services.AddScoped<MeterReadingValidatorService>();

        return services;
    }
}
