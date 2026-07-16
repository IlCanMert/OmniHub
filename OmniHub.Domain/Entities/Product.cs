namespace OmniHub.Domain.Entities;

public class Product : BaseEntity
{
    public Guid TenantId { get; set; }
    public Tenant? Tenant { get; set; }

    public string SKU { get; set; } = string.Empty; // Stok Kodu
    public string Name { get; set; } = string.Empty;
    public int TotalStock { get; set; }
    public decimal Price { get; set; }
}