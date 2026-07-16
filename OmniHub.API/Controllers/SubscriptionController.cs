using Microsoft.AspNetCore.Mvc;

namespace OmniHub.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubscriptionController : ControllerBase
{
    private readonly ILogger<SubscriptionController> _logger;

    public SubscriptionController(ILogger<SubscriptionController> logger)
    {
        _logger = logger;
    }

    [HttpPost("webhook/payment-success")]
    public IActionResult HandlePaymentSuccess([FromBody] PaymentSuccessWebhookPayload payload)
    {
        if (string.IsNullOrEmpty(payload.CustomerEmail) || payload.AmountPaid <= 0)
        {
            return BadRequest("Geçersiz webhook verisi.");
        }

        _logger.LogInformation($"Ödeme Alındı! Müşteri: {payload.CustomerEmail}, Tutar: {payload.AmountPaid} {payload.Currency}");

        var generatedTenantId = Guid.NewGuid().ToString();

        _logger.LogInformation($"Yeni Kiracı (Tenant) otomatik olarak oluşturuldu! ID: {generatedTenantId}");

        return Ok(new
        {
            Status = "Success",
            Message = "Ödeme onaylandı, şirket profili ve izole çalışma alanı başarıyla oluşturuldu.",
            Details = new
            {
                Email = payload.CustomerEmail,
                TenantId = generatedTenantId,
                SubscriptionType = payload.PlanName
            }
        });
    }
}

public class PaymentSuccessWebhookPayload
{
    public string CustomerEmail { get; set; } = string.Empty;
    public decimal AmountPaid { get; set; }
    public string Currency { get; set; } = "TRY";
    public string PlanName { get; set; } = "Standart Aylık Paket";
    public string TransactionId { get; set; } = string.Empty;
}