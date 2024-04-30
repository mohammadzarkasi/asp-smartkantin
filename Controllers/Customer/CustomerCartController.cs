using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using smartkantin.Dto;
using smartkantin.Models;
using smartkantin.Repository;
// using smartkantin.Tools;

namespace smartkantin.Controllers.Customer;

[ApiController]
[Route("/api/customer/cart")]
public class CustomerCartController : ControllerBase
{
    // private readonly UserManager<AppUser> userManager;
    private readonly ICustomerCartRepository customerCartRepository;
    private readonly IFoodRepository foodRepository;

    public CustomerCartController(ICustomerCartRepository customerCartRepository, IFoodRepository foodRepository)
    {
        // this.userManager = userManager;
        this.customerCartRepository = customerCartRepository;
        this.foodRepository = foodRepository;
    }
    [HttpGet]
    public async Task<ActionResult<List<CustomerCartItem>>> GetAll()
    {
        // var user = await SessionTools.GetCurrentUser(userManager, User);
        // if (user == null)
        // {
        //     return BadRequest("session tidak valid");
        // }
        // var items = await customerCartRepository.GetAllByUser(user);
        // return Ok(items);
        throw new NotImplementedException();
    }

    [HttpPost("add")]
    public async Task<ActionResult<Food>> Add([FromBody] NewCartItemDto form)
    {
        // var user = await SessionTools.GetCurrentUser(userManager, User);
        // if (user == null)
        // {
        //     return BadRequest("session tidak valid");
        // }

        // var food = await foodRepository.GetById(form.FoodId);
        // if (food == null)
        // {
        //     return BadRequest("Item tidak valid");
        // }

        // var existingItem = await customerCartRepository.GetOneByUserAndFoodId(user, form.FoodId);

        // if (existingItem != null)
        // {
        //     existingItem.Qty += form.Qty;
        //     var result = await customerCartRepository.Update(existingItem);

        //     if (result == null)
        //     {
        //         return StatusCode(StatusCodes.Status500InternalServerError, "kesalahan menyimpan data");
        //     }
        //     return Ok(result);
        // }
        // else
        // {
        //     var newItem = new CustomerCartItem
        //     {
        //         FoodId = form.FoodId,
        //         Qty = form.Qty,
        //         UserId = user.Id
        //     };

        //     var result = await customerCartRepository.Add(newItem);

        //     if (result == null)
        //     {
        //         return StatusCode(StatusCodes.Status500InternalServerError, "kesalahan menyimpan data");
        //     }
        //     return Ok(result);
        // }
        throw new NotImplementedException();
    }

    [HttpPost("update")]
    public async Task<ActionResult<CustomerCartItem>> Update([FromBody] UpdateCartItemDto form)
    {
        // var user = await SessionTools.GetCurrentUser(userManager, User);
        // if (user == null)
        // {
        //     return BadRequest("session tidak valid");
        // }

        // var cartItem = await customerCartRepository.GetOneByUserAndId(user, form.Id);
        // if (cartItem == null)
        // {
        //     return NotFound("data tidak ditemukan");
        // }

        // cartItem.Qty = form.Qty;

        // var result = await customerCartRepository.Update(cartItem);
        // if (result == null)
        // {
        //     return StatusCode(StatusCodes.Status500InternalServerError, "gagal menyimpan data");
        // }
        // return Ok(result);
        throw new NotImplementedException();
    }

    [HttpGet("delete")]
    public async Task<ActionResult<string>> delete([FromQuery(Name = "id")] Guid id)
    {
        throw new NotImplementedException();
        // var user = await SessionTools.GetCurrentUser(userManager, User);
        // if (user == null)
        // {
        //     return BadRequest("session tidak valid");
        // }
        // var item = await customerCartRepository.GetOneByUserAndId(user, id);
        // if (item == null)
        // {
        //     return NotFound("data tidak ditemukan");
        // }

        // await customerCartRepository.Delete(item);

        // return Ok("deleted");
    }
}