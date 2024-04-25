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
            Id = Guid.NewGuid(),
            CreatedOn = DateTime.Now,
        };

        var orderPerVendors = new List<CustomerOrderPerVendor>();
        foreach(var cart in cartItems)
        {
            var sudahAdaVendor = false;
            foreach(var orderVendor in orderPerVendors)
            {
                if(orderVendor.VendorId == cart.TheFood.VendorId)
                {
                    sudahAdaVendor = true;
                    break;
                }
            }

            if(sudahAdaVendor == false)
            {
                orderPerVendors.Add(new CustomerOrderPerVendor{
                    Id = Guid.NewGuid(),
                    OrderId = newOrder.Id,
                    VendorId = cart.TheFood.VendorId,
                    TotalPrice = 0,
                    CreatedOn = DateTime.Now,
                });
            }
        }

        foreach(var orderVendor in orderPerVendors)
        {
            foreach(var cart in cartItems)
            {
                if(cart.TheFood.VendorId == orderVendor.VendorId)
                {
                    var subtotal = cart.Qty * cart.TheFood.Price;
                    orderVendor.orderDetails.Add(new CustomerOrderDetail{
                        FoodId = cart.FoodId,
                        FoodNameSnapshot = cart.TheFood.Name,
                        Id = Guid.NewGuid(),
                        OrderPerVendorId = orderVendor.Id,
                        PriceSnapshot = cart.TheFood.Price,
                        Qty = cart.Qty,
                        Subtotal = subtotal,
                        VendorNameSnapshot = cart.TheFood.Vendor.Name,
                        CreatedOn = DateTime.Now,
                    });
                    orderVendor.TotalPrice += subtotal;
                }
            }
        }

        newOrder.orderPerVendors = orderPerVendors;
        newOrder.TotalPrice = orderPerVendors.Select(i => i.TotalPrice).Sum();

        await customerOrderRepository.Add(newOrder);


        // var vendors = cartItems.Select(c => c.TheFood.Vendor).Distinct();

        // var orderPerVendor = vendors.Select(v => new CustomerOrderPerVendor{
        //     VendorId = v.Id,
        //     OrderId = newOrder.Id,
        //     orderDetails = cartItems.sele
        // }).ToList();

        // var newOrderDetails = cartItems.Select(cart => new CustomerOrderDetail{

        // }).ToList();
        // var newOrderDetails = cartItems.Select(Func<>);
        // return Ok("cart : " + cartItems.Count());
        // return Ok(cartItems);
        // return Ok(new List<object>(){cartItems, newOrder});

        var result = await customerOrderRepository.GetOneByIdAndCustomer(user, newOrder.Id);
        return Ok(result);

        // return Ok(newOrder);
    }
}