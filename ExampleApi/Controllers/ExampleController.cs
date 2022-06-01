using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExampleApi.Controllers;

[Route("Example")]
[Authorize]
public class ExampleController : ControllerBase
{
    [HttpGet]
    public IActionResult GetClaims()
    {
        return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
    }
}