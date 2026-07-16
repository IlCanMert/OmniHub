using Microsoft.Extensions.Logging;
using OmniHub.Application.Interfaces;

namespace OmniHub.Infrastructure.Services;

public class MockIntegrationService : IIntegrationService
{
    private readonly ILogger<MockIntegrationService> _logger;

    public MockIntegrationService(ILogger<MockIntegrationService> logger)
    {
        _logger = logger;
    }

    public async Task<bool> UpdateStockAsync(string platformName, string sku, int newStock)
    {
        // In production, this would call Trendyol or Shopify APIs via HttpClient.
        // For now, we simulate the operation with a 1-second delay.
        await Task.Delay(1000); 
        
        _logger.LogInformation($"[Mock API] Stock for SKU '{sku}' on {platformName} was updated to {newStock}.");
        
        return true;
    }

    // BUG WAS HERE: changed Task<Task<...>> to Task<IEnumerable<string>>
    public async Task<IEnumerable<string>> FetchNewOrdersAsync(string platformName)
    {
        await Task.Delay(1000);
        
        _logger.LogInformation($"[Mock API] Checking new orders from {platformName}...");
        
        // Since we are already in an async method, we can return List directly (no Task.FromResult needed)
        return new List<string> { $"ORD-{Random.Shared.Next(1000, 9999)}" };
    }
}