using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace OmniHub.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;

    private static readonly Dictionary<string, (string Password, string TenantId)> _users = new();

    public AuthController(IConfiguration configuration)
    {
        _configuration = configuration;
        
        // Default founder account for tests
        if (!_users.ContainsKey("admin@omnihub.com"))
        {
            _users.Add("admin@omnihub.com", ("123456", "dff711f9-03a1-4084-9f18-cfbf3ebb7f09"));
        }
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] RegisterRequest request)
    {
        if (_users.ContainsKey(request.Email))
            return BadRequest("A subscription already exists with this email address.");

        // Database isolation, company ID
        var newTenantId = Guid.NewGuid().ToString();
        
        _users.Add(request.Email, (request.Password, newTenantId));

        return Ok(new 
        { 
            Message = "Payment approved and your account has been created!", 
            TenantId = newTenantId 
        });
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        if (_users.TryGetValue(request.Email, out var user) && user.Password == request.Password)
        {
            var token = GenerateJwtToken(request.Email, user.TenantId);
            return Ok(new { Token = token, Message = "Login successful!" });
        }

        return Unauthorized("Incorrect email or password.");
    }

    private string GenerateJwtToken(string email, string tenantId)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var secretKey = Encoding.ASCII.GetBytes(jwtSettings["Secret"]!);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("TenantId", tenantId)
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(double.Parse(jwtSettings["DurationInMinutes"]!)),
            Issuer = jwtSettings["Issuer"],
            Audience = jwtSettings["Audience"],
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}

public class LoginRequest
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class RegisterRequest
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    // PaymentTransactionId will be added
}