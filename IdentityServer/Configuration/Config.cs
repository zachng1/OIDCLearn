using IdentityServer4;
using IdentityServer4.Models;

namespace IdentityServer.Configuration;

public static class Config
{
    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            new ApiScope("exampleApi", "Example Api")
        };

    public static IEnumerable<Client> Clients =>
        new List<Client>
        {
            new Client
            {
                ClientId = "exampleClient",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets =
                {
                    new Secret("secret".Sha256()) //TODO: get this from config
                },
                AllowedScopes = { "exampleApi" }
            },
            new Client
            {
                ClientId = "mvcClient",
                ClientName = "MVC Client",
                AllowedGrantTypes = GrantTypes.Implicit,
                RedirectUris = { "http://localhost:5205/signin-oidc", "https://localhost:7013/signin-oidc" },
                PostLogoutRedirectUris = { "http://localhost:5205/signout-callback-oidc", "https://localhost:7013/signout-callback-oidc" },
                
                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile
                }
            }
        };
    
    public static IEnumerable<IdentityResource> GetIdentityResources()
    {
        return new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };
    }

}