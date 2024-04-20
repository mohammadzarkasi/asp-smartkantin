using Microsoft.AspNetCore.Mvc;

namespace smartkantin.Controllers.Customer;

[ApiController]
[Route("/api/customer/order")]
public class CustomerOrderController : ControllerBase
{
    [HttpGet]
    public string GetAll()
    {
        return "get all";
    }

    [HttpPost("add")]
    public string Add()
    {
        return "add";
    }
}