using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using smartkantin.Models;
using smartkantin.Repository;
using smartkantin.Tools;
// using smartkantin.Tools;

namespace smartkantin.Controllers.Vendor;

[ApiController]
[Route("/api/vendor/order")]
[Authorize(Roles = "Vendor")]
public class VendorOrderController : ControllerBase
{
    // private readonly UserManager<AppUser> userManager;
    private readonly IVendorRepository vendorRepository;
    private readonly IVendorOrderRepository vendorOrderRepository;

    public VendorOrderController(IVendorRepository vendorRepository, IVendorOrderRepository vendorOrderRepository)
    {
        // this.userManager = userManager;
        this.vendorRepository = vendorRepository;
        this.vendorOrderRepository = vendorOrderRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<CustomerOrderPerVendor>>> GetAll()
    {
        var userId = SessionTools.GetCurrentUserId(User);   
        var vendorMe = await vendorRepository.GetByUserId(userId);
        if(vendorMe == null)
        {
            return BadRequest("Data vendor tidak valid");
        }
        
        var orders = await vendorOrderRepository.GetAllByVendor(vendorMe);

        orders = orders.Select(o => {
            o.Vendor = null;
            o.Order.Customer.Password = "";
            o.Order.Customer.Email = "";
            return o;
        });

        return Ok(orders);
    }

    [HttpGet("detail")]
    public async Task<ActionResult<CustomerOrderPerVendor>> Detail([FromQuery(Name = "id")] Guid id)
    {
        var userId = SessionTools.GetCurrentUserId(User);
        var vendorMe = await vendorRepository.GetByUserId(userId);
        if(vendorMe == null)
        {
            return BadRequest("Data vendor tidak valid");
        }

        var order = await vendorOrderRepository.GetOneByIdAndVendorId(id, vendorMe.Id);

        if(order == null)
        {
            return NotFound("data tidak ditemukan");
        }

        order.Vendor = null;
        order.Order.Customer.Password = "";
        order.Order.Customer.Email = "";

        return Ok(order);
    }
}