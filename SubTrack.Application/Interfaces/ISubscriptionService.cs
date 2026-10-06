using SubTrack.Application.DTOs.Subscriptions;

namespace SubTrack.Application.Interfaces;

public interface ISubscriptionService
{
    Task<CreateSubscriptionResponse> CreateSubscriptionAsync(
        CreateSubscriptionRequest request);

    Task<CreateSubscriptionResponse?> GetSubscriptionByIdAsync(long id);

    Task<IEnumerable<CreateSubscriptionResponse>> GetAllSubscriptionsAsync(
        long? userId,
        bool? isActive);

    Task<CreateSubscriptionResponse?> DeleteSubscriptionAsync(long id);
}