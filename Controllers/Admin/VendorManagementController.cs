using Microsoft.AspNetCore.Mvc;

namespace smartkantin.Controllers.Admin;

[ApiController]
[Route("/api/admin/vendor")]
public class VendorManagementController : ControllerBase
{
    [HttpGet]
    public string GetAll()
    {
        return " get all";
    }
}