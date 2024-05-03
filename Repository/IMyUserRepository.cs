using smartkantin.Dto;
using smartkantin.Models;

namespace smartkantin.Repository;
public interface IMyUserRepository
{
    Task<MyUser?> GetOneByUsername(string username);
    Task<MyUser?> GetOneByEmail(string email);
    Task<MyUser> RegisterNewUser(RegisterDto form);
    Task<MyUser?> GetOneByEmailOrUsername(string emailOrUsername);
    Task<MyUser?> GetOneById(Guid id);
    Task<IEnumerable<MyRole>> GetRolesOfUser(MyUser user);
    Task<IEnumerable<MyUser>> GetAll();
    Task<MyUser?> AssignUserToRole(MyUser user, MyRole role);
    Task<bool> IsUserHasRole(Guid userId, Guid roleId);




    Task RemoveUserFromRole(MyUserRole userRole);
    Task<MyUserRole?> GetOneUserRoleByUserAndRole(MyUser user, MyRole role);
    Task<MyUserRole?> GetOneUserRoleByUserAndRole(Guid userId, Guid roleId);
    Task<MyUserRole?> GetOneUserRoleById(Guid id);
    int CountAdmin();
}