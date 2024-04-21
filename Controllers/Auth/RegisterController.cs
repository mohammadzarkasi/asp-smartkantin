using Microsoft.AspNetCore.Mvc;
using smartkantin.Dto;
using smartkantin.Repository;

namespace smartkantin.Controllers.Auth;


[ApiController]
[Route("/api/auth/register")]
public class RegisterController : ControllerBase
{
    private readonly IMyUserRepository userRepository;

    public RegisterController(IMyUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }
    [HttpPost]
    public async Task<ActionResult<MyUserDto>> Register([FromBody] RegisterDto form)
    {
        var userByEmail = await userRepository.GetOneByEmail(form.Email);
        if (userByEmail != null)
        {
            return StatusCode(StatusCodes.Status400BadRequest, "Email sudah dipakai");
        }

        var userByUsername = await userRepository.GetOneByUsername(form.Username);
        if (userByUsername != null)
        {
            return StatusCode(StatusCodes.Status400BadRequest, "Username sudah dipakai");
        }

        var newUser = await userRepository.RegisterNewUser(form);
        return Ok(MyUserDto.FromMyUser(newUser));
    }
}