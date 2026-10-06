using System.ComponentModel.DataAnnotations;
using SubTrack.Domain.Enums;

namespace SubTrack.Application.DTOs.Subscriptions;

public class CreateSubscriptionRequest
{
    [Range(1, long.MaxValue)]
    public long UserId { get; set; }

    [Required]
    [StringLength(200)]
    public required string Name { get; set; }

    [Range(0.01, double.MaxValue)]
    public decimal Price { get; set; }

    public DateOnly NextBillingDate { get; set; }

    [EnumDataType(typeof(BillingFrequency))]
    public BillingFrequency BillingFrequency { get; set; }
}
