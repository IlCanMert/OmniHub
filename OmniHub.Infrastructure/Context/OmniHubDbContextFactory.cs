using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace OmniHub.Infrastructure.Context;

public class OmniHubDbContextFactory : IDesignTimeDbContextFactory<OmniHubDbContext>
{
    public OmniHubDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<OmniHubDbContext>();
        
        // Connection string for the Docker database (used only for migrations)
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=OmniHubDb;Username=postgres;Password=postgres123");

        return new OmniHubDbContext(optionsBuilder.Options);
    }
}