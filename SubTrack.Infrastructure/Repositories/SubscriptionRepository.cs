using SubTrack.Application.Interfaces;
using SubTrack.Domain.Entities;
using SubTrack.Infrastructure.Data;

namespace SubTrack.Infrastructure.Repositories;

public class SubscriptionRepository : ISubscriptionRepository
{
    private readonly SubTrackDbContext _dbContext;

    public SubscriptionRepository(SubTrackDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Subscription> CreateSubscriptionAsync(
        Subscription subscription)
    {
        _dbContext.Subscriptions.Add(subscription);

        await _dbContext.SaveChangesAsync();

        return subscription;
    }
}