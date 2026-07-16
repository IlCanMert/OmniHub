namespace OmniHub.Domain.Entities;

public class Integration : BaseEntity
{
    public Guid TenantId { get; set; }
    public Tenant? Tenant { get; set; }

    public string PlatformName { get; set; } = string.Empty; // Örn: "Shopify", "Trendyol"
    public string ApiKey { get; set; } = string.Empty;
    public string ApiSecret { get; set; } = string.Empty;
    public string StoreUrl { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
}