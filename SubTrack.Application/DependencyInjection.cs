using Microsoft.Extensions.DependencyInjection;
using SubTrack.Application.Interfaces;
using SubTrack.Application.Services;

namespace SubTrack.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddScoped<ISubscriptionService, SubscriptionService>();

        return services;
    }
}