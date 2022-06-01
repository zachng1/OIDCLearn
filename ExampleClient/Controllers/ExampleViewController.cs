using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;

namespace ExampleClient.Controllers;

[Route("Example")]
public class ExampleViewController : Controller
{
    private HttpClient authClient;
    private HttpClient apiClient;

    public ExampleViewController(IHttpClientFactory factory)
    {
        authClient = factory.CreateClient();
        apiClient = factory.CreateClient();
    }
    /// <summary>
    /// This should show the information in a client's access_token
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetClientClaims()
    {
        var disco = await authClient.GetDiscoveryDocumentAsync("https://localhost:7131");
        if (disco.IsError)
        {
            Console.WriteLine(disco.Error);
            return StatusCode(500);
        }

        var tokenResponse = await authClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
        {
            Address = disco.TokenEndpoint,
            ClientId = "exampleClient",
            ClientSecret = "secret",
            Scope = "exampleApi"
        });
        if (tokenResponse.IsError)
        {
            Console.WriteLine(tokenResponse.Error);
            return StatusCode(500);
        }
        Console.WriteLine(tokenResponse.Json);
        
        apiClient.SetBearerToken(tokenResponse.AccessToken);
        var apiResponse = await apiClient.GetAsync("https://localhost:7154/example");
        if (!apiResponse.IsSuccessStatusCode)
        {
            Console.WriteLine(apiResponse.StatusCode);
            return StatusCode(500);
        }

        var claims = await apiResponse.Content.ReadAsStringAsync();

        return new JsonResult(claims);
    }
}