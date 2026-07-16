using Microsoft.EntityFrameworkCore;
using OmniHub.Domain.Entities;

namespace OmniHub.Application.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Tenant> Tenants { get; }
    DbSet<Product> Products { get; }
    DbSet<Integration> Integrations { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}