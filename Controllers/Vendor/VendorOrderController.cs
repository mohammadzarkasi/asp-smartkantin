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
    public async Task<ActionResult<List<CustomerOrder>>> GetAll()
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
            return o;
        });

        return Ok(orders);
    }
}