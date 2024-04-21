using smartkantin.Models;

namespace smartkantin.Service
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}