using Microsoft.AspNetCore.Mvc;

namespace smartkantin.Controllers.Admin;

[ApiController]
[Route("/api/admin/role")]
public class MasterRoleController : ControllerBase
{
    [HttpGet]
    public string getAll()
    {
        return "get all";
    }

    [HttpPost("add")]
    public string add()
    {
        return "add";
    }
}