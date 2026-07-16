using MediatR;
using OmniHub.Application.Features.Orders.Commands;
using OmniHub.Application.Interfaces;

namespace OmniHub.Infrastructure.Services;

public class OrderSyncService
{
    private readonly IIntegrationService _integrationService;
    private readonly ISender _mediator; // MediatR'ı sipariş komutunu tetiklemek için kullanıyoruz

    public OrderSyncService(IIntegrationService integrationService, ISender mediator)
    {
        _integrationService = integrationService;
        _mediator = mediator;
    }

    // Hangfire'ın her dakika tetikleyeceği metot
    public async Task PollAndProcessOrdersAsync()
    {
        // 1. Trendyol'dan yeni gelen siparişleri çek (Mock API)
        var orders = await _integrationService.FetchNewOrdersAsync("Trendyol");

        // 2. Eğer sipariş varsa (Mock servisimiz her dakika rastgele 1 sipariş kodu üretir)
        foreach (var orderId in orders)
        {
            // Test senaryomuz gereği: Satılan ürünün "KAZAK-KIRMIZI-L" olduğunu simüle ediyoruz.
            // Gerçek projede e-ticaret API'sinden hangi SKU'nun satıldığı bilgisi gelir.
            await _mediator.Send(new ProcessOrderCommand
            {
                PlatformName = "Trendyol",
                SKU = "KAZAK-KIRMIZI-L",
                Quantity = 5 // Her siparişte 5 adet satıldığını varsayalım
            });
        }
    }
}