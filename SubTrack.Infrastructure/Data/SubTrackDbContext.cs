using Microsoft.EntityFrameworkCore;
using SubTrack.Domain.Entities;

namespace SubTrack.Infrastructure.Data;

public class SubTrackDbContext : DbContext
{
    public SubTrackDbContext(DbContextOptions<SubTrackDbContext> options)
        : base(options)
    {
    }

    public DbSet<Subscription> Subscriptions { get; set; }
}
