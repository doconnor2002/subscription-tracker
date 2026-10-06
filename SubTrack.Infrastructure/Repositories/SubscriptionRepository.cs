using Microsoft.EntityFrameworkCore;
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

    public async Task<Subscription> CreateSubscriptionAsync(Subscription subscription)
    {
        _dbContext.Subscriptions.Add(subscription);
        await _dbContext.SaveChangesAsync();

        return subscription;
    }

    public async Task<Subscription?> GetSubscriptionByIdAsync(long id)
    {
        return await _dbContext.Subscriptions
            .FirstOrDefaultAsync(subscription => subscription.Id == id);
    }

    public async Task<IEnumerable<Subscription>> GetAllSubscriptionsAsync(long? userId)
    {
        if (userId.HasValue)
        {
            return await _dbContext.Subscriptions
                .AsNoTracking()
                .Where(subscription => subscription.UserId == userId.Value)
                .ToListAsync();
        }

        return await _dbContext.Subscriptions
            .AsNoTracking()
            .ToListAsync();
    }
}