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
        // Gerçekte burada HttpClient ile Trendyol veya Shopify API'sine istek atılacak.
        // Şimdilik işlemi simüle etmek için 1 saniye bekletiyoruz.
        await Task.Delay(1000); 
        
        _logger.LogInformation($"[Mock API] {platformName} platformunda '{sku}' kodlu ürünün stoku {newStock} olarak güncellendi.");
        
        return true;
    }

    // HATA BURADAYDI: Task<Task<...>> yerine Task<IEnumerable<string>> yaptık
    public async Task<IEnumerable<string>> FetchNewOrdersAsync(string platformName)
    {
        await Task.Delay(1000);
        
        _logger.LogInformation($"[Mock API] {platformName} platformundan yeni siparişler kontrol ediliyor...");
        
        // async metodun içinde olduğumuz için doğrudan List döndürebiliriz (Task.FromResult'a gerek yok)
        return new List<string> { $"ORD-{Random.Shared.Next(1000, 9999)}" };
    }
}