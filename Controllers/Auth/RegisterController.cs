using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using smartkantin.Dto;
using smartkantin.Models;
using smartkantin.Repository;
using smartkantin.Service;

namespace smartkantin.Controllers.Auth;


[ApiController]
[Route("/api/auth/register")]
public class RegisterController : ControllerBase
{
    // private readonly UserManager<AppUser> userManager;
    private readonly ITokenService tokenService;
    private readonly IMyUserRepository userRepository;

    // private readonly IMyUserRepository userRepository;

    public RegisterController(ITokenService tokenService, IMyUserRepository userRepository)
    {
        // this.userManager = userManager;
        this.tokenService = tokenService;
        this.userRepository = userRepository;
        // this.userRepository = userRepository;
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterDto form)
    {

        // throw new NotImplementedException();
        // try
        // {
        if (ModelState.IsValid == false)
        {
            return BadRequest(ModelState);
        }

        var existingUser = await userRepository.GetOneByEmail(form.Email) ?? await userRepository.GetOneByUsername(form.Username);

        if (existingUser != null)
        {
            return BadRequest("email/username sudah dipakai");
        }

        var result = await userRepository.RegisterNewUser(form);

        if (result == null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "terjadi kesalahan memproses data");
        }
        return Ok(MyUserDto.FromMyUser(result));

        //     var appUser = new MyUser
        //     {
        //         Username = form.Username,
        //         Email = form.Email
        //     };

        // var createdUser = await userManager.CreateAsync(appUser, form.Password);

        //     if (createdUser.Succeeded)
        //     {
        //         var roleResult = await userManager.AddToRoleAsync(appUser, "User");
        //         if (roleResult.Succeeded)
        //         {
        //             // var token = tokenService.CreateToken(appUser);
        //             // Console.WriteLine(token);
        //             return Ok("User created");
        //             // return Ok(token);
        //         }
        //         else
        //         {
        //             return StatusCode(StatusCodes.Status500InternalServerError, roleResult.Errors);
        //         }
        //     }
        //     else
        //     {
        //         return StatusCode(StatusCodes.Status400BadRequest, createdUser.Errors);
        //     }
        // }
        // catch (Exception ex)
        // {
        //     return StatusCode(StatusCodes.Status400BadRequest, ex);
        // }
    }

    // [HttpPost]
    // public async Task<ActionResult<MyUserDto>> Register([FromBody] RegisterDto form)
    // {
    //     var userByEmail = await userRepository.GetOneByEmail(form.Email);
    //     if (userByEmail != null)
    //     {
    //         return StatusCode(StatusCodes.Status400BadRequest, "Email sudah dipakai");
    //     }

    //     var userByUsername = await userRepository.GetOneByUsername(form.Username);
    //     if (userByUsername != null)
    //     {
    //         return StatusCode(StatusCodes.Status400BadRequest, "Username sudah dipakai");
    //     }

    //     var newUser = await userRepository.RegisterNewUser(form);
    //     return Ok(MyUserDto.FromMyUser(newUser));
    // }
}