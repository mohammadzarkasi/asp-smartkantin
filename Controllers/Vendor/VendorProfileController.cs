using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using smartkantin.Dto;
using smartkantin.Models;
using smartkantin.Repository;
using smartkantin.Tools;
// using smartkantin.Tools;

namespace smartkantin.Controllers.Vendor;

[ApiController]
[Route("/api/vendor/profile")]
[Authorize(Roles = "Vendor")]
public class VendorProfileController : ControllerBase
{
    // private readonly UserManager<AppUser> userManager;
    private readonly IVendorRepository vendorRepository;

    public VendorProfileController(IVendorRepository vendorRepository)
    {
        // this.userManager = userManager;
        this.vendorRepository = vendorRepository;
    }
    [HttpGet]
    public async Task<ActionResult<VendorAccount>> MyProfile()
    {
        var userId = SessionTools.GetCurrentUserId(User);   
        var vendorMe = await vendorRepository.GetByUserId(userId);
        if(vendorMe == null)
        {
            return BadRequest("Data vendor tidak valid");
        }
        
        vendorMe.User = null;

        return Ok(vendorMe);
        
    }

    [HttpPost("update")]
    public async Task<ActionResult<VendorAccount>> Update([FromBody] NewVendorDto form)
    {
        var userId = SessionTools.GetCurrentUserId(User);   
        var vendorMe = await vendorRepository.GetByUserId(userId);
        if(vendorMe == null)
        {
            return BadRequest("Data vendor tidak valid");
        }
        
        if (vendorMe == null)
        {
            var newProfile = new VendorAccount
            {
                Name = form.Name,
                UserId = userId,
                PictPath = "",
            };
            var result = await vendorRepository.Add(newProfile);
            if (result == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Gagal menyimpan data");
            }
            result.User = null;
            return Ok(result);
        }
        else
        {
            vendorMe.Name = form.Name;
            var result = await vendorRepository.Update(vendorMe);
            if (result == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Gagal menyimpan data");
            }
            result.User = null;
            return Ok(result);
        }
    }
}