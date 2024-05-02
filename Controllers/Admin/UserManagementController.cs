using Microsoft.AspNetCore.Mvc;
using smartkantin.Dto;
using smartkantin.Models;
using smartkantin.Repository;

namespace smartkantin.Controllers.Admin;

[ApiController]
[Route("/api/admin/user-management")]
public class UserManagementController : ControllerBase
{
    private readonly IMyUserRepository userRepository;
    private readonly IRoleRepository roleRepository;

    public UserManagementController(IMyUserRepository userRepository, IRoleRepository roleRepository)
    {
        this.userRepository = userRepository;
        this.roleRepository = roleRepository;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MyUserDto>>> getAll()
    {
        var r = await userRepository.GetAll();
        return Ok(r.Select(i => MyUserDto.FromMyUser(i)));
    }

    [HttpPost("add-role")]
    public async Task<ActionResult<MyUserDto>> addRoleToUser([FromBody] AssignUserToRoleDto form)
    {
        if (ModelState.IsValid == false)
        {
            return BadRequest(ModelState);
        }
        var user = await userRepository.GetOneById(form.UserId);
        if (user == null)
        {
            return NotFound("user tidak ditemukan");
        }
        var role = await roleRepository.GetOneById(form.RoleId);
        if (role == null)
        {
            return NotFound("role tidak ditemukan");
        }
        var r = await userRepository.AssignUserToRole(user, role);
        if (r == null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "kesalahan menyimpan data");
        }
        return Ok(MyUserDto.FromMyUser(r));
    }
}