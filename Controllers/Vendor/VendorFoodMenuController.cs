using Microsoft.AspNetCore.Mvc;
using smartkantin.Repository;

namespace smartkantin.Controllers.Vendor;

[ApiController]
[Route("/api/vendor/food")]
public class VendorFoodMenuController : ControllerBase
{
    private readonly IFoodRepository foodRepository;

    public VendorFoodMenuController(IFoodRepository foodRepository)
    {
        this.foodRepository = foodRepository;
    }
    [HttpGet]
    public string GetAll()
    {
        return "get all";
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