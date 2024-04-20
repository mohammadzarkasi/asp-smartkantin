using Microsoft.AspNetCore.Mvc;

namespace smartkantin.Controllers.Vendor;

[ApiController]
[Route("/api/vendor/food")]
public class VendorFoodMenuController : ControllerBase
{
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