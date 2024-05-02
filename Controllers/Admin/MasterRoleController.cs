using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using smartkantin.Models;
using smartkantin.Repository;

namespace smartkantin.Controllers.Admin;

[ApiController]
[Route("/api/admin/role")]
[Authorize(Roles = "Admin")]
public class MasterRoleController : ControllerBase
{
    private readonly IRoleRepository roleRepository;

    public MasterRoleController(IRoleRepository roleRepository)
    {
        this.roleRepository = roleRepository;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MyRole>>> getAll()
    {
        var roles = await roleRepository.GetAll();
        roles = roles.Select(r =>
        {
            r.Users = [];
            return r;
        });
        return Ok(roles);
    }
}