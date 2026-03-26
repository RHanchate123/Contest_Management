using System.Globalization;

namespace PrismatchMiddleware.API.JWT;

public class JwtConfiguration
{
    public string Issuer { get; } = string.Empty;

    public string SecretKey { get; } = string.Empty;

    public string Audience { get; } = string.Empty;

    public int ExpireDays { get; }

    public JwtConfiguration(IConfiguration configuration)
    {
        var section = configuration.GetSection("JwtSettings");

        Issuer = section[nameof(Issuer)] ?? string.Empty;
        SecretKey = section[nameof(SecretKey)] ?? string.Empty;
        Audience = section[nameof(Audience)] ?? string.Empty;
        ExpireDays = Convert.ToInt32(section[nameof(ExpireDays)], CultureInfo.InvariantCulture);
    }
}