using SubTrack.Application.DTOs.Subscriptions;
using SubTrack.Application.Interfaces;
using SubTrack.Domain.Entities;

namespace SubTrack.Application.Services;

public class SubscriptionService : ISubscriptionService
{
    private readonly ISubscriptionRepository _repository;

    public SubscriptionService(ISubscriptionRepository repository)
    {
        _repository = repository;
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

        var createdSubscription =
            await _repository.CreateSubscriptionAsync(subscription);

        return new CreateSubscriptionResponse
        {
            Id = createdSubscription.Id,
            UserId = createdSubscription.UserId,
            Name = createdSubscription.Name,
            Price = createdSubscription.Price,
            NextBillingDate = createdSubscription.NextBillingDate,
            BillingFrequency = createdSubscription.BillingFrequency,
            IsActive = createdSubscription.IsActive,
            CreatedAt = createdSubscription.CreatedAt
        };
    }

    public async Task<CreateSubscriptionResponse?> GetSubscriptionByIdAsync(long id)
    {
        var subscription = await _repository.GetSubscriptionByIdAsync(id);

        if (subscription is null)
        {
            return null;
        }

        return new CreateSubscriptionResponse
        {
            Id = subscription.Id,
            UserId = subscription.UserId,
            Name = subscription.Name,
            Price = subscription.Price,
            NextBillingDate = subscription.NextBillingDate,
            BillingFrequency = subscription.BillingFrequency,
            IsActive = subscription.IsActive,
            CreatedAt = subscription.CreatedAt
        };
    }
}