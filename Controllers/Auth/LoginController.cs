using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using smartkantin.Dto;
using smartkantin.Models;
using smartkantin.Repository;
using smartkantin.Service;

namespace smartkantin.Controllers.Auth;

[ApiController]
[Route("/api/auth/login")]
public class LoginController : ControllerBase
{
    // private readonly UserManager<AppUser> userManager;
    private readonly ITokenService tokenService;
    private readonly IMyUserRepository userRepository;

    // private readonly SignInManager<AppUser> signInManager;

    // public LoginController(UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signInManager)
    public LoginController(ITokenService tokenService, IMyUserRepository userRepository)
    {
        // this.userManager = userManager;
        this.tokenService = tokenService;
        this.userRepository = userRepository;
        // this.signInManager = signInManager;
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginDto form)
    {
        if (ModelState.IsValid == false)
        {
            return BadRequest(ModelState);
        }

        // return NotFound();
        // var user = await userManager.Users
        //     .Where(item => item.UserName == form.UserName)
        //     .FirstOrDefaultAsync();
        var user = await userRepository.GetOneByEmailOrUsername(form.Username);

        if (user == null)
        {
            Console.WriteLine("user tidak ditemukan");
            return Unauthorized("Username/Password tidak cocok");
        }

        var isPasswordMatched = BCrypt.Net.BCrypt.EnhancedVerify(form.Password, user.Password);
        if (isPasswordMatched == false)
        {
            Console.WriteLine("password tidak cocok, hashed: " + user.Password);

            // Console.WriteLine("new key: " + BCrypt.Net.BCrypt.EnhancedHashPassword("123", 13));
            // Console.WriteLine("new key: " + BCrypt.Net.BCrypt.EnhancedHashPassword("123", 13));
            // Console.WriteLine("new key: " + BCrypt.Net.BCrypt.EnhancedHashPassword("123", 13));

            return Unauthorized("Username/Password tidak cocok");
        }

        // var loginResult = await signInManager.CheckPasswordSignInAsync(user, form.Password, false);

        // if (loginResult.Succeeded == false)
        // {
        //     return Unauthorized("Username/Password Incorrect");
        // }

        var token = tokenService.CreateToken(user);

        return Ok(token);
    }

    [HttpGet("test-role")]
    public IActionResult testRole()
    {
        var l = new List<object>(){
            User.Identity,
            User.Identity.IsAuthenticated,
            User.Identity.Name,
            User.Identity.AuthenticationType,
            User.IsInRole("Admin"),
        };
        return Ok(l);
    }

    [HttpGet("check")]
    public IActionResult checkStatusLogin()
    {
        var result = new Dictionary<string, bool>();
        if(User?.Identity?.IsAuthenticated == true)
        {
            result.Add("status", true);
        }
        else
        {
            result.Add("status", false);
        }
        return Ok(result);
    }
}