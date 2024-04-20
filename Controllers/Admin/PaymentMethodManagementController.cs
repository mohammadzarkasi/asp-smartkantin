using Microsoft.AspNetCore.Mvc;

namespace smartkantin.Controllers.Admin;

[ApiController]
[Route("/api/admin/payment-method", Name = "Management Payment Method")]
public class PaymentMethodManagementController : ControllerBase
{
    [HttpGet]
    public string getAll()
    {
        return "get All";
    }

    [HttpPost("add")]
    public string add()
    {
        return "add";
    }
}