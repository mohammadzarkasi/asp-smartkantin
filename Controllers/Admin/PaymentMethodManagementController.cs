using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using smartkantin.Models;
using smartkantin.Repository;

namespace smartkantin.Controllers.Admin;

[ApiController]
[Route("/api/admin/payment-method", Name = "Management Payment Method")]
[Authorize(Policy = "Admin")]
public class PaymentMethodManagementController : ControllerBase
{
    private readonly IPaymentMethodRepository paymentMethodRepository;

    public PaymentMethodManagementController(IPaymentMethodRepository paymentMethodRepository)
    {
        this.paymentMethodRepository = paymentMethodRepository;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PaymentMethod>>> getAll()
    {
        var r= await paymentMethodRepository.GetAll();
        return Ok(r);
    }

    [HttpPost("add")]
    public string add()
    {
        return "add";
    }
}