using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace Mooc.Core;

public static class MoocCoreExtension
{
    public static void AddAppCore(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["JwtSetting:Issuer"],
                    ValidAudience = configuration["JwtSetting:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["JwtSetting:SecurityKey"]!)),
                    NameClaimType = "firstName",
                    RoleClaimType = ClaimTypes.Role
                };
            });

        services.AddAuthorizationCore();
        services.AddMemoryCache();
        services.AddDistributedMemoryCache();

        var redisEnabled = configuration["Caching:UseRedis"];
        if (string.IsNullOrEmpty(redisEnabled) || bool.Parse(redisEnabled))
        {
            var redisConfiguration = configuration["Caching:Configuration"];
            if (!string.IsNullOrEmpty(redisConfiguration))
            {
                var instanceName = configuration["Caching:InstanceName"];
                if (string.IsNullOrEmpty(instanceName))
                    instanceName = "moccdb";

                services.AddStackExchangeRedisCache(option =>
                {
                    option.Configuration = redisConfiguration;
                    option.InstanceName = instanceName;
                });
            }
        }
    }
}
