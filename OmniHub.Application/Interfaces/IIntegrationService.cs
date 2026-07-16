namespace OmniHub.Application.Interfaces;

public interface IIntegrationService
{
    Task<bool> UpdateStockAsync(string platformName, string sku, int newStock);
    
    Task<IEnumerable<string>> FetchNewOrdersAsync(string platformName);
}