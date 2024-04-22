using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

    public VendorFoodMenuController(IFoodRepository foodRepository, UserManager<AppUser> userManager)
    {
        this.foodRepository = foodRepository;
        this.userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var user = await SessionTools.GetCurrentUser(userManager, User);
        // return Ok("get all");
        return Ok(user);
    }

    [HttpPost("add")]
    public string Add()
    {
        return "add";
    }

    [HttpPost("update")]
    public string Update()
    {
        return "update";
    }
}