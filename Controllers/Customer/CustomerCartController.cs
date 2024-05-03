using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using smartkantin.Dto;
using smartkantin.Models;
using smartkantin.Repository;
using smartkantin.Tools;
// using smartkantin.Tools;

namespace smartkantin.Controllers.Customer;

[ApiController]
[Route("/api/customer/cart")]
[Authorize(Roles = "Customer")]
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
        var id = SessionTools.GetCurrentUserId(User);
        var items = await customerCartRepository.GetAllByUserId(id);
        items = items.Select(i => {
            i.User = null;
            return i;
        });
        return Ok(items);
        // throw new NotImplementedException();
    }

    [HttpPost("add")]
    public async Task<ActionResult<Food>> Add([FromBody] NewCartItemDto form)
    {
        if(ModelState.IsValid == false)
        {
            return BadRequest(ModelState);
        }

        var userId = SessionTools.GetCurrentUserId(User);
        
        var food = await foodRepository.GetById(form.FoodId);
        if (food == null)
        {
            return BadRequest("Item tidak valid");
        }

        var existingItem = await customerCartRepository.GetOneByFoodIdAndUserId(form.FoodId, userId);

        if (existingItem != null)
        {
            existingItem.Qty += form.Qty;
            var result = await customerCartRepository.Update(existingItem);

            if (result == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "kesalahan menyimpan data");
            }
            result.User = null;
            return Ok(result);
        }
        else
        {
            var newItem = new CustomerCartItem
            {
                FoodId = form.FoodId,
                Qty = form.Qty,
                UserId = userId
            };

            var result = await customerCartRepository.Add(newItem);

            if (result == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "kesalahan menyimpan data");
            }
            result.User = null;
            return Ok(result);
        }
        // throw new NotImplementedException();
    }

    [HttpPost("update")]
    public async Task<ActionResult<CustomerCartItem>> Update([FromBody] UpdateCartItemDto form)
    {
        if(ModelState.IsValid == false)
        {
            return BadRequest(ModelState);
        }

        var userId = SessionTools.GetCurrentUserId(User);
        

        // var cartItem = await customerCartRepository.GetOneByUserAndId(user, form.Id);
        var cartItem = await customerCartRepository.GetOneByIdAndUserId(form.Id, userId);
        if (cartItem == null)
        {
            return NotFound("data tidak ditemukan");
        }

        cartItem.Qty = form.Qty;

        var result = await customerCartRepository.Update(cartItem);
        if (result == null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "gagal menyimpan data");
        }

        result.User = null;

        return Ok(result);
        // throw new NotImplementedException();
    }

    [HttpGet("delete")]
    public async Task<ActionResult<string>> delete([FromQuery(Name = "id")] Guid id)
    {
        // throw new NotImplementedException();
        var userId = SessionTools.GetCurrentUserId(User);

        var item = await customerCartRepository.GetOneByIdAndUserId( id, userId);
        if (item == null)
        {
            return NotFound("data tidak ditemukan");
        }

        await customerCartRepository.Delete(item);

        return Ok("deleted");
    }
}