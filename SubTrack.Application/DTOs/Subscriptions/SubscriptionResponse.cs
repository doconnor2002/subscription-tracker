using SubTrack.Domain.Enums;

namespace SubTrack.Application.DTOs.Subscriptions;

public class SubscriptionResponse
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public string Name { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public DateOnly NextBillingDate { get; set; }

    public BillingFrequency BillingFrequency { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }
}