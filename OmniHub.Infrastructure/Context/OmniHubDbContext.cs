using Microsoft.EntityFrameworkCore;
using OmniHub.Application.Interfaces; // Added this line
using OmniHub.Domain.Entities;

namespace OmniHub.Infrastructure.Context;

// Added IApplicationDbContext interface
public class OmniHubDbContext : DbContext, IApplicationDbContext 
{
    public OmniHubDbContext(DbContextOptions<OmniHubDbContext> options) : base(options)
    {
    }

    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Integration> Integrations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}