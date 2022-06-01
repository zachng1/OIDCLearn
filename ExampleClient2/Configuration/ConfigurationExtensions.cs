using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Logging;

namespace ExampleClient2.Configuration;

public static class ConfigurationExtensions
{
    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddControllersWithViews();
        services.AddMvc(options =>
        {
            options.EnableEndpointRouting = false;
        });
        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

        services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc";
            })
            .AddCookie("Cookies")
            .AddOpenIdConnect("oidc", options =>
            {
                options.Authority = "https://localhost:7131";
                options.RequireHttpsMetadata = false;
                options.ClientId = "mvcClient";
                options.SaveTokens = true;
            });
        IdentityModelEventSource.ShowPII = true;
    }

    public static void ConfigureApplication(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseAuthentication();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseMvcWithDefaultRoute();
    }
}