using Microsoft.AspNetCore.Mvc;

namespace smartkantin.Controllers.Customer;

[ApiController]
[Route("/api/customer/payment")]
public class CustomerPaymentController : ControllerBase
{
    [HttpGet]
    public string GetAll()
    {
        return "get all";
    }
}