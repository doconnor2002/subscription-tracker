using SubTrack.Application.DTOs.Subscriptions;
using SubTrack.Application.Interfaces;
using SubTrack.Domain.Entities;

namespace SubTrack.Application.Services;

public class SubscriptionService : ISubscriptionService
{
    private readonly ISubscriptionRepository _subscriptionRepository;

    public SubscriptionService(ISubscriptionRepository subscriptionRepository)
    {
        _subscriptionRepository = subscriptionRepository;
    }

    public async Task<CreateSubscriptionResponse> CreateSubscriptionAsync(
        CreateSubscriptionRequest request)
    {
        var subscription = new Subscription
        {
            UserId = request.UserId,
            Name = request.Name,
            Price = request.Price,
            BillingFrequency = request.BillingFrequency,
            NextBillingDate = request.NextBillingDate,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        var savedSubscription =
            await _subscriptionRepository.CreateSubscriptionAsync(subscription);

        return new CreateSubscriptionResponse
        {
            Id = savedSubscription.Id,
            UserId = savedSubscription.UserId,
            Name = savedSubscription.Name,
            Price = savedSubscription.Price,
            BillingFrequency = savedSubscription.BillingFrequency,
            NextBillingDate = savedSubscription.NextBillingDate,
            IsActive = savedSubscription.IsActive,
            CreatedAt = savedSubscription.CreatedAt
        };
    }
}