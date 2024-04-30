// using System.Security.Claims;
// using Microsoft.AspNetCore.Identity;
// using smartkantin.Models;

// namespace smartkantin.Tools
// {
//     public class SessionTools
//     {
//         internal static async Task<AppUser?> GetCurrentUser(UserManager<AppUser> userManager, ClaimsPrincipal claimPrincipal)
//         {
//             var userIdClaim = claimPrincipal.Claims.Where(claim => claim.Type == "user_id").FirstOrDefault();
//             var userId = userIdClaim?.Value ?? "";
//             Console.WriteLine("user id from claims: " + userId);
//             var appUser = await userManager.FindByIdAsync(userId);

//             return appUser;
//         }
//     }
// }