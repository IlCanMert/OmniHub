using MediatR;
using Microsoft.AspNetCore.Mvc;
using OmniHub.Application.Features.Tenants.Commands;

namespace OmniHub.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TenantsController : ControllerBase
{
    private readonly IMediator _mediator;

    public TenantsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTenant([FromBody] CreateTenantCommand command)
    {
        var tenantId = await _mediator.Send(command);
        return Ok(new { Message = "Müşteri başarıyla oluşturuldu!", TenantId = tenantId });
    }
}