using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IdentityServer.Pages.Login;

public class LoginModel 
{
    [BindProperty]
    [Required] 
    public string Username { get; set; }
    
    [BindProperty]
    [Required]
    public string Password { get; set; }
    
    [BindProperty]
    [Required]
    public string ReturnUrl { get; set; }
}