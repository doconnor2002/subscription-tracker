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

    public async Task<IEnumerable<Subscription>> GetAllSubscriptionsAsync(
        long? userId,
        bool? isActive)
    {
        var query = _dbContext.Subscriptions
            .AsNoTracking();

        if (userId.HasValue)
        {
            query = query.Where(subscription => subscription.UserId == userId.Value);
        }

        if (isActive.HasValue)
        {
            query = query.Where(subscription => subscription.IsActive == isActive.Value);
        }

        return await query.ToListAsync();
    }
}