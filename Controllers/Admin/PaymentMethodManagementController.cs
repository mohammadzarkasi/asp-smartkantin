using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using smartkantin.Dto;
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
        var r = await paymentMethodRepository.GetAll();
        return Ok(r);
    }

    [HttpPost("add")]
    public async Task<ActionResult<PaymentMethod>> add([FromBody] NewPaymentMethodDto form)
    {
        if (ModelState.IsValid == false)
        {
            return BadRequest(ModelState);
        }

        var existings = await paymentMethodRepository.GetManyByCode(form.code);

        if (existings.Any())
        {
            return BadRequest("code sudah dipakai");
        }

        var r = await paymentMethodRepository.Add(new PaymentMethod
        {
            Name = form.name,
            Code = form.code,
            NeedConfirmation = form.NeedConfirmation,
            NeedUploadPayment = form.NeedUploadPayment,
        });

        if (r == null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "gagal menyimpan data");
        }
        return Ok(r);
    }

    [HttpGet("delete")]
    public async Task<IActionResult> delete([FromQuery(Name = "id")] Guid id)
    {
        var p = await paymentMethodRepository.GetOneById(id);
        if (p == null)
        {
            return NotFound("tidak ditemukan");
        }
        await paymentMethodRepository.Delete(p);
        return Ok("deleted");
    }

    [HttpPost("update")]
    public async Task<ActionResult<PaymentMethod>> update([FromQuery(Name = "id")] Guid id, [FromBody] NewPaymentMethodDto form)
    {
        if (ModelState.IsValid == false)
        {
            return BadRequest(ModelState);
        }

        var p = await paymentMethodRepository.GetOneById(id);
        if (p == null)
        {
            return NotFound("tidak ditemukan");
        }

        var existings = await paymentMethodRepository.GetManyByCode(form.code);
        if (existings.Count() > 1)
        {
            return BadRequest("code sudah dipakai");
        }
        if (existings.Any())
        {
            var ex1 = existings.ElementAt(0);
            if (ex1.Id != p.Id)
            {
                return BadRequest("code sudah dipakai");
            }
        }

        p.Code = form.code;
        p.Name = form.name;
        p.NeedConfirmation = form.NeedUploadPayment;
        p.NeedUploadPayment = form.NeedUploadPayment;

        var r = await paymentMethodRepository.Update(p);

        if (r == null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "gagal menyimpan data");
        }

        return Ok(r);
    }
}