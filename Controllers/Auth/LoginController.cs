using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using smartkantin.Dto;
using smartkantin.Models;
using smartkantin.Service;

namespace smartkantin.Controllers.Auth;

[ApiController]
[Route("/api/auth/login")]
public class LoginController : ControllerBase
{
    private readonly UserManager<AppUser> userManager;
    private readonly ITokenService tokenService;
    private readonly SignInManager<AppUser> signInManager;

    public LoginController(UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signInManager)
    {
        this.userManager = userManager;
        this.tokenService = tokenService;
        this.signInManager = signInManager;
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginDto form)
    {
        if(ModelState.IsValid == false)
        {
            return BadRequest(ModelState);
        }    
        var user = await userManager.Users
            .Where(item => item.UserName == form.UserName)
            .FirstOrDefaultAsync();
        
        if (user == null)
        {
            return Unauthorized("Invalid Username");
        }

        var loginResult = await signInManager.CheckPasswordSignInAsync(user, form.Password, false);

        if(loginResult.Succeeded == false)
        {
            return Unauthorized("Username/Password Incorrect");
        }

        var token = tokenService.CreateToken(user);

        return Ok(token);
    }
}