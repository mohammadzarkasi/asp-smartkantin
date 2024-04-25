using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using smartkantin.Models;
using smartkantin.Repository;
using smartkantin.Tools;

namespace smartkantin.Controllers.Customer;

[ApiController]
[Route("/api/customer/order")]
public class CustomerOrderController : ControllerBase
{
    private readonly UserManager<AppUser> userManager;
    private readonly ICustomerOrderRepository customerOrderRepository;
    private readonly ICustomerCartRepository customerCartRepository;

    public CustomerOrderController(UserManager<AppUser> userManager, ICustomerOrderRepository customerOrderRepository, ICustomerCartRepository customerCartRepository)
    {
        this.userManager = userManager;
        this.customerOrderRepository = customerOrderRepository;
        this.customerCartRepository = customerCartRepository;
    }
    [HttpGet]
    public async Task<ActionResult<List<CustomerOrder>>> GetAll()
    {
        var user = await SessionTools.GetCurrentUser(userManager, User);
        if (user == null)
        {
            return BadRequest("session tidak valid");
        }
        var result = await customerOrderRepository.GetAllByCustomer(user);
        return Ok(result);
    }

    [HttpGet("add")]
    public async Task<IActionResult> Add()
    {
        var user = await SessionTools.GetCurrentUser(userManager, User);
        if (user == null)
        {
            return BadRequest("session tidak valid");
        }
        
        var cartItems = await customerCartRepository.GetAllByUser(user);
        if(cartItems.IsNullOrEmpty() == true)
        {
            return BadRequest("cart masih kosong");
        }

        var newOrder = new CustomerOrder{
            CustomerId = user.Id,
        };

        var vendors = cartItems.Select(c => c.TheFood.Vendor).Distinct();

        var orderPerVendor = vendors.Select(v => new CustomerOrderPerVendor{
            VendorId = v.Id
        }).ToList();

        // var newOrderDetails = cartItems.Select(cart => new CustomerOrderDetail{

        // }).ToList();
        // var newOrderDetails = cartItems.Select(Func<>);
        // return Ok("cart : " + cartItems.Count());
        return Ok(cartItems);
    }
}