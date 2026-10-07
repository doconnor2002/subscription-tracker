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

    public async Task<SubscriptionResponse> CreateSubscriptionAsync(
        CreateSubscriptionRequest request)
    {
        var subscription = new Subscription
        {
            UserId = request.UserId,
            Name = request.Name,
            Price = request.Price,
            NextBillingDate = request.NextBillingDate,
            BillingFrequency = request.BillingFrequency,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        var createdSubscription =
            await _repository.CreateSubscriptionAsync(subscription);

        return MapToResponse(createdSubscription);
    }

    public async Task<SubscriptionResponse?> GetSubscriptionByIdAsync(long id)
    {
        var subscription = await _repository.GetSubscriptionByIdAsync(id);

        if (subscription is null)
        {
            return null;
        }

        return MapToResponse(subscription);
    }

    public async Task<IEnumerable<SubscriptionResponse>> GetAllSubscriptionsAsync(
        long? userId,
        bool? isActive)
    {
        var subscriptions =
            await _repository.GetAllSubscriptionsAsync(userId, isActive);

        return subscriptions.Select(MapToResponse);
    }

    public async Task<bool> DeleteSubscriptionAsync(long id)
    {
        return await _repository.DeleteSubscriptionAsync(id);
    }

    private static SubscriptionResponse MapToResponse(Subscription subscription)
    {
        return new SubscriptionResponse
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