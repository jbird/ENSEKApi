using ENSEKApi.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ENSEKApi.Services.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddTransient<IMeterReadingService, MeterReadingService>();
        services.AddScoped<IMeterReadingValidatorService, MeterReadingValidatorService>();

        return services;
    }
}
