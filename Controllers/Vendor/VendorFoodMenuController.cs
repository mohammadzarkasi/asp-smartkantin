using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using smartkantin.Dto;
using smartkantin.Models;
using smartkantin.Repository;
using smartkantin.Tools;
// using smartkantin.Tools;

namespace smartkantin.Controllers.Vendor;

[ApiController]
[Route("/api/vendor/food")]
[Authorize(Roles = "Vendor")]
public class VendorFoodMenuController : ControllerBase
{
    private readonly IFoodRepository foodRepository;
    // private readonly UserManager<AppUser> userManager;
    private readonly IVendorRepository vendorRepository;

    public VendorFoodMenuController(IFoodRepository foodRepository, IVendorRepository vendorRepository)
    {
        this.foodRepository = foodRepository;
        // this.userManager = userManager;
        this.vendorRepository = vendorRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<Food>>> GetAll()
    {
        var userId = SessionTools.GetCurrentUserId(User);   
        var vendorMe = await vendorRepository.GetByUserId(userId);
        if(vendorMe == null)
        {
            return BadRequest("Data vendor tidak valid");
        }
        var result = await foodRepository.GetAllByVendorId(vendorMe.Id);

        result = result.Select(i => {
            i.Vendor = null;
            return i;
        });

        return Ok(result);
    }

    [HttpPost("add")]
    public async Task<ActionResult<Food>> Add([FromBody] NewFoodDto form)
    {
        var userId = SessionTools.GetCurrentUserId(User);   
        var vendorMe = await vendorRepository.GetByUserId(userId);
        if(vendorMe == null)
        {
            return BadRequest("Data vendor tidak valid");
        }
        
        var newFood = new Food
        {
            Name = form.Name,
            Price = form.Price,
            VendorId = vendorMe.Id,
            FoodPict = "",
        };

        var result = await foodRepository.Add(newFood);

        if(result == null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "kesalahan menyimpan data");
        }

        result.Vendor = null;

        return Ok(result);
    }


    [HttpPost("update")]
    public async Task<ActionResult<Food>> Update([FromBody] NewFoodDto form, [FromQuery(Name = "id")] Guid id)
    {
        var userId = SessionTools.GetCurrentUserId(User);   
        var vendorMe = await vendorRepository.GetByUserId(userId);
        if(vendorMe == null)
        {
            return BadRequest("Data vendor tidak valid");
        }
        
        var food = await foodRepository.GetByIdAndVendorId(id, vendorMe.Id);
        if (food == null)
        {
            return NotFound("data tidak ditemukan");
        }

        food.Name = form.Name;
        food.Price = form.Price;

        var result = await foodRepository.Update(food);

        if (result == null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "gagal update");
        }

        result.Vendor = null;

        return Ok(result);
    }

    [HttpGet("delete")]
    public async Task<IActionResult> DeleteFood([FromQuery(Name = "id")] Guid id)
    {
        var userId = SessionTools.GetCurrentUserId(User);   
        var vendorMe = await vendorRepository.GetByUserId(userId);
        if(vendorMe == null)
        {
            return BadRequest("Data vendor tidak valid");
        }
        
        var food = await foodRepository.GetByIdAndVendorId(id, vendorMe.Id);
        if (food == null)
        {
            return NotFound("data tidak ditemukan");
        }

        await foodRepository.FlagDelete(food);

        return Ok("ok");
    }

    [HttpGet("detail")]
    public async Task<ActionResult<Food>> GetOne([FromQuery(Name = "id")] Guid id)
    {
        var userId = SessionTools.GetCurrentUserId(User);   
        var vendorMe = await vendorRepository.GetByUserId(userId);
        if(vendorMe == null)
        {
            return BadRequest("Data vendor tidak valid");
        }

        var food = await foodRepository.GetByIdAndVendorId(id, vendorMe.Id);
        if (food == null)
        {
            return NotFound("data tidak ditemukan");
        }

        food.Vendor = null;

        return Ok(food);
    }
}