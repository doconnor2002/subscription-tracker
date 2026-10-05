using SubTrack.Domain.Entities;

namespace SubTrack.Application.Interfaces;

public interface ISubscriptionRepository
{
    Task<Subscription> CreateSubscriptionAsync(Subscription subscription);
}