using smartkantin.Dto;
using smartkantin.Models;

namespace smartkantin.Repository;
public interface IMyUserRepository
{
    Task<MyUser?> GetOneByUsername(string username);
    Task<MyUser?> GetOneByEmail(string email);
    Task<MyUser> RegisterNewUser(RegisterDto form);
    Task<MyUser?> GetOneByEmailOrUsername(string emailOrUsername);

}