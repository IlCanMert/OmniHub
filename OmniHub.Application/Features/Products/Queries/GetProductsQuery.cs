using MediatR;
using Microsoft.EntityFrameworkCore;
using OmniHub.Application.Interfaces;
using OmniHub.Domain.Entities;

namespace OmniHub.Application.Features.Products.Queries;

public class GetProductsQuery : IRequest<List<ProductDto>>
{
}

public class ProductDto
{
    public Guid Id { get; set; }
    public string SKU { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int TotalStock { get; set; }
    public decimal Price { get; set; }
}

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, List<ProductDto>>
{
    private readonly IApplicationDbContext _context;

    public GetProductsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Products
            .Select(p => new ProductDto
            {
                Id = p.Id,
                SKU = p.SKU,
                Name = p.Name,
                TotalStock = p.TotalStock,
                Price = p.Price
            })
            .ToListAsync(cancellationToken);
    }
}