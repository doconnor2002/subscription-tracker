using System;
using SubTrack.Domain.Enums;

namespace SubTrack.Domain.Entities
{
    public class Subscription
    {
        public long Id {get; set;}
        
        public required long UserId {get; set;}

        public required string Name {get; set;}

        public required decimal Price {get; set;}

        public required BillingFrequency BillingFrequency {get; set;}

        public required DateOnly NextBillingDate {get; set;}

        public bool IsActive {get; set;}

        public DateTime CreatedAt {get; set;}
    }
}
