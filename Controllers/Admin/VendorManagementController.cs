using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace smartkantin.Controllers.Admin;

[ApiController]
[Route("/api/admin/vendor")]
[Authorize(Roles = "Admin")]
public class VendorManagementController : ControllerBase
{
    [HttpGet]
    public string GetAll()
    {
        return " get all";
    }
}