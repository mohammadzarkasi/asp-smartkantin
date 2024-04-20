using Microsoft.AspNetCore.Mvc;

namespace smartkantin.Controllers.Vendor;

[ApiController]
[Route("/api/vendor/profile")]
public class VendorProfileController : ControllerBase
{
    [HttpGet]
    public string MyProfile()
    {
        return "my profile";
    }

    [HttpPost]
    public string Update()
    {
        return "update";
    }
}