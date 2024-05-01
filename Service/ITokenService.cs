using System.Security.Claims;
using smartkantin.Models;

namespace smartkantin.Service
{
    public interface ITokenService
    {
        string CreateToken(MyUser user);
        IEnumerable<Claim> GetClaimsFromToken(string token);
    }
}