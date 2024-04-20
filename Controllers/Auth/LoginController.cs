using Microsoft.AspNetCore.Mvc;

namespace smartkantin.Controllers.Auth;

[ApiController]
[Route("/api/auth/login")]
public class LoginController : ControllerBase
{
    [HttpPost]
    public string login()
    {
        return "login";
    }
}