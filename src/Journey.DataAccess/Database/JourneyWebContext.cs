using Journey.Core.Identity;
using Journey.DataAccess.Common;
using Journey.DataAccess.Entities;
using Journey.Core.Services;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Journey.DataAccess.Database;

public class JourneyWebContext : IdentityDbContext<ApplicationUser>
{
    private readonly IClaimService _claimService;
    public JourneyWebContext(DbContextOptions<JourneyWebContext> options, IClaimService claimService)
        : base(options)
    {
        _claimService = claimService;
    }
    public DbSet<Place> Places { get; set; }
    public new async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        foreach (var entry in ChangeTracker.Entries<IAuditedEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedByUserId = _claimService.GetUserId();
                    entry.Entity.CreatedOn = DateTime.Now;
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedByUserId = _claimService.GetUserId();
                    entry.Entity.UpdatedOn = DateTime.Now;
                    break;
            }
        }
        return await base.SaveChangesAsync(cancellationToken);
    }
}
