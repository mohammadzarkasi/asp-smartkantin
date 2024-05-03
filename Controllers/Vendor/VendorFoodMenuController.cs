using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using smartkantin.Dto;
using smartkantin.Models;
using smartkantin.Repository;
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
        throw new NotImplementedException();
        // var user = await SessionTools.GetCurrentUser(userManager, User);
        // if (user == null)
        // {
        //     return BadRequest("user tidak ditemukan, " + user);
        // }
        // var vendorMe = await vendorRepository.GetByUserId(user?.Id ?? "");
        // var result = await foodRepository.GetAllByVendor(vendorMe?.Id ?? Guid.Empty);
        // // return Ok("get all");
        // return Ok(result);
    }

    [HttpPost("add")]
    public async Task<ActionResult<Food>> Add([FromBody] NewFoodDto form)
    {
        throw new NotImplementedException();
        // var user = await SessionTools.GetCurrentUser(userManager, User);
        // if (user == null)
        // {
        //     return BadRequest("user tidak ditemukan, " + user);
        // }
        // var vendorMe = await vendorRepository.GetByUserId(user?.Id ?? "");
        // if (vendorMe == null)
        // {
        //     return BadRequest("Data Vendor tidak valid");
        // }
        // // return "add";
        // var newFood = new Food
        // {
        //     CreatedOn = DateTime.Now,
        //     Name = form.Name,
        //     Price = form.Price,
        //     VendorId = vendorMe.Id,
        //     FoodPict = "",
        // };

        // var result = await foodRepository.Add(newFood);

        // return Ok(result);
    }


    [HttpPost("update")]
    public async Task<ActionResult<Food>> Update([FromBody] NewFoodDto form, [FromQuery(Name = "id")] Guid id)
    {
        throw new NotImplementedException();
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

        // var food = await foodRepository.GetByIdAndVendorId(id, vendorMe.Id);
        // if (food == null)
        // {
        //     return NotFound("data tidak ditemukan");
        // }

        // food.Name = form.Name;
        // food.Price = form.Price;

        // var result = await foodRepository.Update(food);

        // if (result == null)
        // {
        //     return StatusCode(StatusCodes.Status500InternalServerError, "gagal update");
        // }
        // return Ok(result);
    }

    [HttpGet("delete")]
    public async Task<IActionResult> DeleteFood([FromQuery(Name = "id")] Guid id)
    {
        throw new NotImplementedException();
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

        // var food = await foodRepository.GetByIdAndVendorId(id, vendorMe.Id);
        // if (food == null)
        // {
        //     return NotFound("data tidak ditemukan");
        // }

        // await foodRepository.FlagDelete(food);

        // return Ok("ok");
    }

    [HttpGet("detail")]
    public async Task<ActionResult<Food>> GetOne([FromQuery(Name = "id")] Guid id)
    {
        throw new NotImplementedException();
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

        // var food = await foodRepository.GetByIdAndVendorId(id, vendorMe.Id);
        // if (food == null)
        // {
        //     return NotFound("data tidak ditemukan");
        // }

        // return Ok(food);
    }
}