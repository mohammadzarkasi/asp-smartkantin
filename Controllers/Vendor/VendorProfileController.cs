using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using smartkantin.Dto;
using smartkantin.Models;
using smartkantin.Repository;
// using smartkantin.Tools;

namespace smartkantin.Controllers.Vendor;

[ApiController]
[Route("/api/vendor/profile")]
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
        throw new NotImplementedException();
        // var user = await SessionTools.GetCurrentUser(userManager, User);
        // if (user == null)
        // {
        //     return BadRequest("user tidak ditemukan, " + user);
        // }
        // var vendorAccount = await vendorRepository.GetByUserId(user?.Id ?? "");
        // if (vendorAccount == null)
        // {
        //     return NotFound("data vendor tidak ditemukan");
        // }
        // return Ok(vendorAccount);
    }

    [HttpPost]
    public async Task<ActionResult<VendorAccount>> Update([FromBody] NewVendorDto form)
    {
        throw new NotImplementedException();
        // var user = await SessionTools.GetCurrentUser(userManager, User);
        // if (user == null)
        // {
        //     return BadRequest("user tidak ditemukan, " + user);
        // }
        // var vendorAccount = await vendorRepository.GetByUserId(user?.Id ?? "");
        // if (vendorAccount == null)
        // {
        //     var newProfile = new VendorAccount
        //     {
        //         Name = form.Name,
        //         CreatedOn = DateTime.Now,
        //         UserId = user?.Id ?? "",
        //         PictPath = "",
        //     };
        //     var result = await vendorRepository.Add(newProfile);
        //     if (result == null)
        //     {
        //         return StatusCode(StatusCodes.Status500InternalServerError, "Gagal menyimpan data");
        //     }
        //     return Ok(result);
        // }
        // else
        // {
        //     vendorAccount.Name = form.Name;
        //     var result = await vendorRepository.Update(vendorAccount);
        //     if (result == null)
        //     {
        //         return StatusCode(StatusCodes.Status500InternalServerError, "Gagal menyimpan data");
        //     }
        //     return Ok(result);
        // }
    }
}