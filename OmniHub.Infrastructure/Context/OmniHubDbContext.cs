using Microsoft.EntityFrameworkCore;
using OmniHub.Application.Interfaces; // Bu satırı ekledik
using OmniHub.Domain.Entities;

namespace OmniHub.Infrastructure.Context;

// IApplicationDbContext arayüzünü (interface) ekledik
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