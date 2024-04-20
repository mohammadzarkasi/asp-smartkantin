using Microsoft.AspNetCore.Mvc;

namespace smartkantin.Controllers.Customer;

[ApiController]
[Route("/api/customer/cart")]
public class CustomerCartController : ControllerBase
{
    [HttpGet]
    public string GetAll()
    {
        return " get all";
    }

    [HttpPost("add")]
    public string Add()
    {
        return " add";
    }

    [HttpPost("update")]
    public string Update()
    {
        return "update";
    }

    [HttpPost("delete")]
    public string delete()
    {
        return "delete";
    }
}