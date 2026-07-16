using MediatR;
using OmniHub.Application.Features.Orders.Commands;
using OmniHub.Application.Interfaces;

namespace OmniHub.Infrastructure.Services;

public class OrderSyncService
{
    private readonly IIntegrationService _integrationService;
    private readonly ISender _mediator; // Used to trigger the order command via MediatR

    public OrderSyncService(IIntegrationService integrationService, ISender mediator)
    {
        _integrationService = integrationService;
        _mediator = mediator;
    }

    // Method triggered every minute by Hangfire
    public async Task PollAndProcessOrdersAsync()
    {
        // 1. Pull new incoming orders from Trendyol (Mock API)
        var orders = await _integrationService.FetchNewOrdersAsync("Trendyol");

        // 2. If there are orders (our mock service generates 1 random order code every minute)
        foreach (var orderId in orders)
        {
            // For this test scenario: we simulate that the sold product is "KAZAK-KIRMIZI-L".
            // In a real project, the sold SKU would come from the e-commerce API.
            await _mediator.Send(new ProcessOrderCommand
            {
                PlatformName = "Trendyol",
                SKU = "KAZAK-KIRMIZI-L",
                Quantity = 5 // Assume 5 units are sold per order
            });
        }
    }
}