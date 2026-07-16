namespace OmniHub.Domain.Entities;

public class Tenant : BaseEntity
{
    public string CompanyName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? SubscriptionPlan { get; set; } // Örn: "Basic", "Pro"
    public bool IsActive { get; set; } = true;
    
    // Bir müşterinin birden fazla ürünü ve entegrasyonu olabilir
    public ICollection<Product> Products { get; set; } = new List<Product>();
    public ICollection<Integration> Integrations { get; set; } = new List<Integration>();
}