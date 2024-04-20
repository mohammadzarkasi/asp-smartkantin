using Microsoft.AspNetCore.Mvc;

namespace smartkantin.Controllers;

[ApiController]
[Route("/api/public/food")]
public class FoodMenuController : ControllerBase
{
    [HttpGet]
    public string GetTopMenu()
    {
        return " get top ";
    }
}