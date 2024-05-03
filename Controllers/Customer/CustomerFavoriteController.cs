using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace smartkantin.Controllers.Customer;


[ApiController]
[Route("/api/customer/favorite")]
[Authorize(Roles = "Customer")]
public class CustomerFavoriteController : ControllerBase
{
    [HttpGet]
    public string GetAll()
    {
        return "get all";
    }
}