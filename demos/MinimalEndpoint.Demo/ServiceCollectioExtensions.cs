using Microsoft.AspNetCore.Authentication.Cookies;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace MinimalEndpoint.Demo;

public static class ServiceCollectioExtensions
{

    public static IServiceCollection AddAuth(this IServiceCollection services)
    {
        services.AddAuthorization(o =>
        {
            o.AddPolicy("AdminsOnly", b => b.RequireClaim(ClaimTypes.Role, "admin"));
        });

        services
        .AddAuthentication(o =>
        {
            o.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            o.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        })
        .AddCookie(options =>
        {
            options.LoginPath = "/fake-login-page";
            options.AccessDeniedPath = "/access-denied";
        });

        return services;
    }
}
