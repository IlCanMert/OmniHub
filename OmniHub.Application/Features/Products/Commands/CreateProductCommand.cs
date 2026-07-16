using MediatR;
using OmniHub.Application.Interfaces;
using OmniHub.Domain.Entities;

namespace OmniHub.Application.Features.Products.Commands;

public class CreateProductCommand : IRequest<Guid>
{
    public Guid TenantId { get; set; }
    public string SKU { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int TotalStock { get; set; }
    public decimal Price { get; set; }
}

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly IIntegrationService _integrationService;

    public CreateProductCommandHandler(IApplicationDbContext context, IIntegrationService integrationService)
    {
        _context = context;
        _integrationService = integrationService;
    }

    public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            TenantId = request.TenantId,
            SKU = request.SKU,
            Name = request.Name,
            TotalStock = request.TotalStock,
            Price = request.Price
        };

        _context.Products.Add(product);
        await _context.SaveChangesAsync(cancellationToken);

        await _integrationService.UpdateStockAsync("Trendyol", product.SKU, product.TotalStock);
        await _integrationService.UpdateStockAsync("Shopify", product.SKU, product.TotalStock);

        return product.Id;
    }
}