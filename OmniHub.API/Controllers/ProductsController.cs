using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization; // Required library
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
        // Incoming request goes directly to MediatR
        // MediatR finds and runs the relevant handler and returns the new product ID
        var productId = await _mediator.Send(command);
        
        return Ok(new { Message = "Product added successfully!", ProductId = productId });
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        var products = await _mediator.Send(new GetProductsQuery());
        return Ok(products);
    }
}