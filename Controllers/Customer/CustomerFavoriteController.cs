using Microsoft.AspNetCore.Mvc;

namespace smartkantin.Controllers.Customer;


[ApiController]
[Route("/api/customer/favorite")]
public class CustomerFavoriteController : ControllerBase
{
    [HttpGet]
    public string GetAll()
    {
        return "get all";
    }
}