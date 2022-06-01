using IdentityServer.Pages.Login;
using IdentityServer4;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Controllers;

[Route("Login")]
public class LoginController : Controller
{
    
    private readonly IIdentityServerInteractionService _interaction;
    private readonly IAuthenticationSchemeProvider _schemeProvider;

    public LoginController(IIdentityServerInteractionService interaction, IAuthenticationSchemeProvider schemeProvider)
    {
        _interaction = interaction;
        _schemeProvider = schemeProvider;
    }

    [HttpGet]
    public IActionResult Login(string returnUrl)
    {
        var model = new LoginModel
        {
            ReturnUrl = returnUrl
        };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginModel model, string button)
    {
        var context = await _interaction.GetAuthorizationContextAsync(model.ReturnUrl);
        if (model.Username != "admin" || model.Password != "password") return StatusCode(500); // TODO: Security
        
        await HttpContext.SignInAsync(new IdentityServerUser("subjectId"));
        return Redirect(model.ReturnUrl);

    }
}