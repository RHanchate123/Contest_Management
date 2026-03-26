using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace PrismatchMiddleware.API.JWT;

public static class JwtAuthBuilderExtesnions
{
    public static AuthenticationBuilder AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtConfiguration = new JwtConfiguration(configuration);

        services.AddAuthorization(options =>
        {
            options.AddPolicy("Admin", policy =>
                policy.RequireRole("Admin"));

            options.AddPolicy("VIP", policy =>
                policy.RequireRole("VIP"));

            options.AddPolicy("Guest", policy =>
                policy.RequireRole("Guest"));

            options.AddPolicy("Signedin", policy =>
                policy.RequireRole("Signedin"));
        });

        return services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(x =>
        {
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtConfiguration.Issuer,
                ValidAudience = jwtConfiguration.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfiguration.SecretKey)),
                ClockSkew = TimeSpan.Zero, //Removes the default 5-minute grace period

                RequireExpirationTime = true,
            };
            x.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    var raw = context.Request.Headers["Authorization"];
                    Console.WriteLine("Authorization header: " + raw);

                    if (!string.IsNullOrEmpty(raw))
                    {
                        context.Token = raw.ToString().Replace("Bearer ", "", StringComparison.OrdinalIgnoreCase);
                    }

                    return Task.CompletedTask;
                },
                OnAuthenticationFailed = context =>
                {
                    Console.WriteLine("JWT validation failed: " + context.Exception.Message);
                    return Task.CompletedTask;
                }
            };
        });
    }
}