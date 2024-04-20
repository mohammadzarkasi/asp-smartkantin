using Microsoft.AspNetCore.Mvc;

namespace smartkantin.Controllers.Auth;


[ApiController]
[Route("/api/auth/register")]
public class RegisterController : ControllerBase
{
    [HttpPost]
    public string register()
    {
        return "register";
    }
}