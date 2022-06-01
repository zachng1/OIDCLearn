using IdentityServer4;
using IdentityServer4.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace IdentityServer.Configuration;

public static class ConfigurationExtensions
{
    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddIdentityServer(options =>
            {
                options.UserInteraction.LoginUrl= "/login";
            })
            .AddInMemoryIdentityResources(Config.GetIdentityResources())
            .AddInMemoryApiScopes(Config.ApiScopes)
            .AddInMemoryClients(Config.Clients)
            .AddDeveloperSigningCredential();
        services.AddMvc(options => options.EnableEndpointRouting = false);

        services.AddAuthentication()
            .AddOpenIdConnect("oidc", "Demo IdServer", options =>
            {
                options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                options.SignOutScheme = IdentityServerConstants.SignoutScheme;
                options.SaveTokens = true;

                options.Authority = "https://demo.identityserver.io/";
                options.ClientId = "interactive.confidential";
                options.ClientSecret = "secret";
                options.ResponseType = "code";

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = "name",
                    RoleClaimType = "role"
                };
            });

    }

    public static void ConfigureApp(this IApplicationBuilder app)
    {
        app.UseMvc();
        app.UseDeveloperExceptionPage();
        app.UseIdentityServer();
    }
}