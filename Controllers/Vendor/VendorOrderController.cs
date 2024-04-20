using Microsoft.AspNetCore.Mvc;

namespace smartkantin.Controllers.Vendor;

[ApiController]
[Route("/api/vendor/order")]
public class VendorOrderController : ControllerBase
{
    [HttpGet]
    public string GetAll()
    {
        return "get all";
    }
}