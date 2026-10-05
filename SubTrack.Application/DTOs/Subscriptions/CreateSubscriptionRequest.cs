using SubTrack.Domain.Enums;

namespace SubTrack.Application.DTOs.Subscriptions;

public class CreateSubscriptionRequest
{
    public long UserId { get; set; }
    public required string Name { get; set; }
    public decimal Price { get; set; }
    public DateOnly NextBillingDate { get; set; }
    public BillingFrequency BillingFrequency { get; set; }
}
