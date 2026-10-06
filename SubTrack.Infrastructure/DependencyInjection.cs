using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SubTrack.Application.Interfaces;
using SubTrack.Infrastructure.Data;
using SubTrack.Infrastructure.Repositories;

namespace SubTrack.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<SubTrackDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("SubTrack")));

        services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();

        return services;
    }
}