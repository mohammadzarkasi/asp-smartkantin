using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using smartkantin.Dto;
using smartkantin.Models;
using smartkantin.Repository;
using smartkantin.Tools;

namespace smartkantin.Controllers.Vendor;

[ApiController]
[Route("/api/vendor/food")]
[Authorize]
public class VendorFoodMenuController : ControllerBase
{
    private readonly IFoodRepository foodRepository;
    private readonly UserManager<AppUser> userManager;
    private readonly IVendorRepository vendorRepository;

    public VendorFoodMenuController(IFoodRepository foodRepository, UserManager<AppUser> userManager, IVendorRepository vendorRepository)
    {
        this.foodRepository = foodRepository;
        this.userManager = userManager;
        this.vendorRepository = vendorRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<Food>>> GetAll()
    {
        var user = await SessionTools.GetCurrentUser(userManager, User);
        var vendorMe = await vendorRepository.GetByUserId(user?.Id ?? "");
        var result = await foodRepository.GetAllByVendor(vendorMe?.Id ?? Guid.Empty);
        // return Ok("get all");
        return Ok(result);
    }

    [HttpPost("add")]
    public async Task<ActionResult<Food>> Add([FromBody] NewFoodDto form)
    {
        var user = await SessionTools.GetCurrentUser(userManager, User);
        var vendorMe = await vendorRepository.GetByUserId(user?.Id ?? "");
        if (vendorMe == null)
        {
            return BadRequest("Data Vendor tidak valid");
        }
        // return "add";
        var newFood = new Food
        {
            CreatedAt = DateTime.Now,
            Name = form.Name,
            Price = form.Price,
            VendorId = vendorMe.Id
        };

        var result = await foodRepository.Add(newFood);

        return Ok(result);
    }

    [HttpPost("update")]
    public string Update()
    {
        return "update";
    }
}