using Microsoft.AspNetCore.Mvc;
using smartkantin.Models;
using smartkantin.Repository;
using smartkantin.Tools;

namespace smartkantin.Controllers.Auth;

[ApiController]
[Route("api/auth/profile")]
public class UserController : ControllerBase
{
    private readonly IMyUserRepository userRepository;

    public UserController(IMyUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    [HttpGet]
    public async Task<ActionResult<MyUser>> GetProfile()
    {
        var userId = SessionTools.GetCurrentUserId(User);
        var user = await userRepository.GetOneById(userId);
        return Ok(user);
    }
}