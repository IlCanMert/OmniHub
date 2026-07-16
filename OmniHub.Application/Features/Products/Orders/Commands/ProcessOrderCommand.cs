using MediatR;
using Microsoft.EntityFrameworkCore;
using OmniHub.Application.Interfaces;

namespace OmniHub.Application.Features.Orders.Commands;

//Dışarıdan gelen sipariş verisi
public class ProcessOrderCommand : IRequest<bool>
{
    public string PlatformName { get; set; } = string.Empty;
    public string SKU { get; set; } = string.Empty;
    public int Quantity { get; set; }
}

//Siparişi işleyen Handler
public class ProcessOrderCommandHandler : IRequestHandler<ProcessOrderCommand, bool>
{
    private readonly IApplicationDbContext _context;
    private readonly IIntegrationService _integrationService;

    public ProcessOrderCommandHandler(IApplicationDbContext context, IIntegrationService integrationService)
    {
        _context = context;
        _integrationService = integrationService;
    }

    public async Task<bool> Handle(ProcessOrderCommand request, CancellationToken cancellationToken)
    {
        //Sipariş edilen ürünü SKU koduna göre bulur
        var product = await _context.Products
            .FirstOrDefaultAsync(p => p.SKU == request.SKU, cancellationToken);

        if (product == null) return false;

        //Stok kontrolü
        if (product.TotalStock < request.Quantity) return false;

        product.TotalStock -= request.Quantity;
        await _context.SaveChangesAsync(cancellationToken);

        //OTOMATİK EŞİTLEME
        string targetPlatform = request.PlatformName == "Trendyol" ? "Shopify" : "Trendyol";
        await _integrationService.UpdateStockAsync(targetPlatform, product.SKU, product.TotalStock);

        return true;
    }
}