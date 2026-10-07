using SubTrack.Application.DTOs.Subscriptions;

namespace SubTrack.Application.Interfaces;

public interface ISubscriptionService
{
    Task<SubscriptionResponse> CreateSubscriptionAsync(
        CreateSubscriptionRequest request);

    Task<SubscriptionResponse?> GetSubscriptionByIdAsync(long id);

    Task<IEnumerable<SubscriptionResponse>> GetAllSubscriptionsAsync(
        long? userId,
        bool? isActive);

    Task<bool> DeleteSubscriptionAsync(long id);
}