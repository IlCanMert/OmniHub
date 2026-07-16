using MediatR;
using OmniHub.Application.Interfaces;
using OmniHub.Domain.Entities;

namespace OmniHub.Application.Features.Tenants.Commands;

public class CreateTenantCommand : IRequest<Guid>
{
    public string CompanyName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}

public class CreateTenantCommandHandler : IRequestHandler<CreateTenantCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateTenantCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateTenantCommand request, CancellationToken cancellationToken)
    {
        var tenant = new Tenant
        {
            CompanyName = request.CompanyName,
            Email = request.Email,
            SubscriptionPlan = "Basic"
        };

        _context.Tenants.Add(tenant);
        await _context.SaveChangesAsync(cancellationToken);

        return tenant.Id;
    }
}