namespace OmniHub.Domain.Entities;

public class Tenant : BaseEntity
{
    public string CompanyName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? SubscriptionPlan { get; set; } // e.g.: "Basic", "Pro"
    public bool IsActive { get; set; } = true;
    
    // A customer can have multiple products and integrations
    public ICollection<Product> Products { get; set; } = new List<Product>();
    public ICollection<Integration> Integrations { get; set; } = new List<Integration>();
}