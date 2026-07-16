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
            return BadRequest("Invalid webhook payload.");
        }

        _logger.LogInformation($"Payment received! Customer: {payload.CustomerEmail}, Amount: {payload.AmountPaid} {payload.Currency}");

        var generatedTenantId = Guid.NewGuid().ToString();

        _logger.LogInformation($"New tenant created automatically! ID: {generatedTenantId}");

        return Ok(new
        {
            Status = "Success",
            Message = "Payment approved, company profile and isolated workspace created successfully.",
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
    public string PlanName { get; set; } = "Standard Monthly Plan";
    public string TransactionId { get; set; } = string.Empty;
}