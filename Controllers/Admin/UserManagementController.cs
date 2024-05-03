using Microsoft.AspNetCore.Mvc;
using smartkantin.Dto;
using smartkantin.Models;
using smartkantin.Repository;
using smartkantin.Tools;

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

        var userAlreadyHasThisRole = await userRepository.IsUserHasRole(user.Id, role.Id);

        if(userAlreadyHasThisRole == true)
        {
            return BadRequest("user sudah memiliki role ini");
        }

        var r = await userRepository.AssignUserToRole(user, role);
        if (r == null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "kesalahan menyimpan data");
        }
        return Ok(MyUserDto.FromMyUser(r));
    }

    [HttpGet("remove-from-role")]
    public async Task<IActionResult> removeUserFromRole([FromQuery(Name = "id")] Guid userroleId)
    {
        if(ModelState.IsValid==false)
        {
            return BadRequest(ModelState);
        }

        var ur = await userRepository.GetOneUserRoleById(userroleId);
        if(ur == null)
        {
            return NotFound("data tidak ditemukan");
        }

        string myId = User.Claims.Where(c => c.Type == "user_id").FirstOrDefault()?.Value ?? "";
        var myGuid = GuidHelper.ParseGuid(myId);

        if(myGuid == ur.UserId && ur.Role.Name == "Admin")
        {
            var countAdmin = userRepository.CountAdmin();
            if(countAdmin == 1)
            {
                return BadRequest("user admin tidak dapat dihapus");
            }
        }

        await userRepository.RemoveUserFromRole(ur);

        return Ok("user removed from the role");
    }
}