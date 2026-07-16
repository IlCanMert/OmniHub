using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization; // Gerekli kütüphane
using OmniHub.Application.Features.Products.Commands;
using OmniHub.Application.Features.Products.Queries;

namespace OmniHub.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command)
    {
        // Gelen istek doğrudan MediatR'a gider
        // MediatR ilgili Handler'ı bulup çalıştırır ve bize yeni ürünün ID'sini döndürür
        var productId = await _mediator.Send(command);
        
        return Ok(new { Message = "Ürün başarıyla eklendi!", ProductId = productId });
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        var products = await _mediator.Send(new GetProductsQuery());
        return Ok(products);
    }
}