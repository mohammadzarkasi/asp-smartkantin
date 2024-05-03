using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using smartkantin.Models;
using smartkantin.Repository;
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
        Console.WriteLine("user: " + User?.Identity?.IsAuthenticated);
        return NotFound();
        // var user = await SessionTools.GetCurrentUser(userManager, User);
        // if (user == null)
        // {
        //     return BadRequest("session tidak valid");
        // }

        // var vendorMe = await vendorRepository.GetByUserId(user.Id);
        // if (vendorMe == null)
        // {
        //     return BadRequest("data vendor tidak valid");
        // }

        // var orders = await vendorOrderRepository.GetAllByVendor(vendorMe);
        // return Ok(orders);
    }
}