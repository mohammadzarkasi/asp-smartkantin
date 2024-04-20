using Microsoft.AspNetCore.Mvc;

namespace smartkantin.Controllers.Admin;

[ApiController]
[Route("/api/admin/user-management")]
public class UserManagementController : ControllerBase
{
    [HttpGet]
    public string getAll()
    {
        return "get all";
    }

    [HttpPost("add-role")]
    public string addRoleToUser()
    {
        return "add role to user";
    }
}