using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace smartkantin.Controllers.Customer;

[ApiController]
[Route("/api/customer/payment")]
[Authorize(Roles = "Customer")]
public class CustomerPaymentController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok("get All");
    }
}